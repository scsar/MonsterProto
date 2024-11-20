using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector]
    public bool isFinished = false; // 스킬이 종료되었음을 체크하는 bool 구문

    void Update()
    {
        
    }

    public void ActiveSkill(float damage, Transform target, int stageIdx)
    {
        isFinished = false;
        // 현재 몬스터가 어떤스테이지에 존재하는지 파악하고 이에따라 사용할수있는 스킬을 제한한다.
        switch (stageIdx)
        {
            case 1:
                StartCoroutine(WaitFire(target, 5));
                break;
            case 2:
                StartCoroutine(MultiFire(target, 5));
                break;
            case 3:
                GameObject tor = Instantiate(windPrefeb);
                break;

        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
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
            Vector3 direction = lines[i].transform.eulerAngles;
            Debug.Log(direction);
            GameObject attack = Instantiate(attackPrefeb);
            attack.transform.position = lines[i].transform.position;
            attack.GetComponent<Projectile>().Pdir = direction.normalized;

        }
        isFinished = true;

    }
}
