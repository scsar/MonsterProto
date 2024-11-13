using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureSkill : MonoBehaviour
{
    // 스테이지에 따라 사용할수있는 패턴 제한
    // Creature스크립트에서 공격 실행하면 skill스크립트에서 패턴 가져와서 수행

    [SerializeField]
    private GameObject attackPrefeb;
    [SerializeField]
    private GameObject linePrefeb;

    void Update()
    {
        
    }

    public void ActiveSkill(float damage, Transform target, int stageIdx)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        // 현재 몬스터가 어떤스테이지에 존재하는지 파악하고 이에따라 사용할수있는 스킬을 제한한다.
        switch (stageIdx)
        {
            case 1:
                GameObject attack = Instantiate(attackPrefeb);
                attack.transform.position = transform.position;
                attack.GetComponent<Projectile>().Pdir = dir;
                break;
            case 2:
                Vector3 direction = target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                GameObject line = Instantiate(linePrefeb);
                line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
                line.transform.SetParent(transform);
                line.transform.localPosition = Vector3.zero;
                StartCoroutine(Wait(2f));
                GameObject attack2 = Instantiate(attackPrefeb);
                attack2.transform.position = transform.position;
                attack2.GetComponent<Projectile>().Pdir = dir;
                break;
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
