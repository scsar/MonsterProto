using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class CreatureSkill : MonoBehaviour
{
    // 스테이지에 따라 사용할수있는 패턴 제한
    // Creature스크립트에서 공격 실행하면 skill스크립트에서 패턴 가져와서 수행

    [SerializeField]
    private GameObject attackPrefeb;
    [SerializeField]
    private GameObject linePrefeb;
    [SerializeField]
    private GameObject windPrefeb;
    [SerializeField]
    private GameObject rangeAttackPrefeb;
    [HideInInspector]
    public bool isFinished = false; // 스킬이 종료되었음을 체크하는 bool 구문


    public void ActiveSkill(float damage, Transform target, int creatureNum)
    {
        isFinished = false;
        // 현재 몬스터의 번호에따라 사용할수있는 스킬을 제한한다.
        switch (creatureNum)
        {
            case 1:
                StartCoroutine(WaitFire(target, 5));
                break;
            case 2:
                int randnum = Random.Range(0, 10);
                if (randnum < 6)
                {
                    StartCoroutine(MultiFire(target, 5));
                }
                else
                {
                    Instantiate(windPrefeb,transform.position, Quaternion.identity);
                    isFinished =true;
                }
                break;
            case 3:
                GameObject rangeAttack = Instantiate(rangeAttackPrefeb, target.position, Quaternion.identity);
                rangeAttack.transform.Translate(0, -0.4f, 0);
                isFinished = true;
                break;
            case 4:
                // 근접 돌진 몬스터(몬스터 정면에 플레이어 or 늑대가 포착될경우, 빠른속도로 이동하며 몸통박치기 함)
                if (SearchMelee()) 
                {
                    StartCoroutine(MeleeAttack((GetComponent<Creature>().isMovingRight ? 1f : -1f)));
                }
                else
                {
                    isFinished = true;
                }
                break;
            case 5:
                // 근접 방어 몬스터 (플레이어가 정면에 포착될경우 방어자세 취한다. )
                if (SearchMelee()) 
                {
                    StartCoroutine(GuardMelee());
                }
                else
                {
                    isFinished = true;
                }
                break;
            case 6:
                // 근접 공격 몬스터 (그냥 심플하게 접근하고 공격)
                StartCoroutine(MeleeNormalAttack(target));
                break;
        }
    }

    IEnumerator WaitFire(Transform target, float time)
    {

        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject line = Instantiate(linePrefeb);

        line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        line.transform.SetParent(transform);
        line.transform.localPosition = Vector3.zero;

        for (float i = time; i > 0; i -= 1)
        {
            line.SetActive(false);
            direction = target.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
            yield return new WaitForSeconds(0.1f * i );
            line.SetActive(true);
            yield return new WaitForSeconds(0.1f * i );
        }

        GameObject attack = Instantiate(attackPrefeb);
        attack.transform.position = transform.position;
        attack.GetComponent<Projectile>().Pdir = direction.normalized;
        
        isFinished = true;
        Destroy(line);
    }

    IEnumerator MultiFire(Transform target, int count)
    {
        List<GameObject> lines = new List<GameObject>();
        for (float i = 0; i < count; i+= 1)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject line = Instantiate(linePrefeb);

            line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 60f + (15f * i)));
            line.transform.SetParent(transform);
            line.transform.localPosition = Vector3.zero;
            lines.Add(line);

            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1f);
        // 리스트에 저장한 라인 각각 꺼내서 pop하면서 발사.
        for (int i = 0; i < count; i++)
        {
            float lineAngle = (lines[i].transform.eulerAngles.z - 90f) * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(lineAngle), Mathf.Sin(lineAngle), 0);
            
            GameObject attack = Instantiate(attackPrefeb);
            attack.transform.position = lines[i].transform.position;
            attack.GetComponent<Projectile>().Pdir = direction;

            yield return new WaitForSeconds(0.5f);
            Destroy(lines[i]);
        }
        isFinished = true;

    }

    IEnumerator MeleeAttack(float movedir)
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("Attack");
        GetComponent<Animator>().SetBool("isAttack",true);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(5 * movedir, 1), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isFinished = true;
    }

    IEnumerator GuardMelee()
    {
        GetComponent<Animator>().SetBool("isGuard", true);
        GetComponent<Animator>().SetTrigger("Guard");
        //TODO
        // 몬스터 피격 기능 구현완료시 무적 기능 삽입
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetBool("isGuard", false);
        isFinished = true;
    }

    IEnumerator MeleeNormalAttack(Transform target)
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.position)) < 4f)
        {
            Debug.Log("melee");
            // GetComponent<Animator>().SetTrigger("Attack");
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        isFinished = true;
    }

    private bool SearchMelee()
    {
        Vector2 origin = new Vector2(transform.GetChild(1).position.x, transform.GetChild(1).transform.position.y);
        Vector2 forwardDirection = GetComponent<Creature>().isMovingRight ? transform.right : -transform.right;
        RaycastHit2D groundInfo = Physics2D.Raycast(origin, forwardDirection, 20f);
        Debug.DrawRay(origin, forwardDirection * 20f, Color.red); // 디버그용 Ray 그리기
        // 지면 체크
        if (groundInfo.collider != null && groundInfo.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
