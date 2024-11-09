using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureSkill : MonoBehaviour
{
    // 스테이지에 따라 사용할수있는 패턴 제한
    // Creature스크립트에서 공격 실행하면 skill스크립트에서 패턴 가져와서 수행

    [SerializeField]
    private GameObject attackPrefeb;

    public void ActiveSkill(float damage, Transform target, int stageIdx)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        Debug.Log(dir);
        // 현재 몬스터가 어떤스테이지에 존재하는지 파악하고 이에따라 사용할수있는 스킬을 제한한다.
        switch (stageIdx)
        {
            case 1:
                GameObject attack = Instantiate(attackPrefeb);
                attack.transform.position = transform.position;
                // attack.transform.rotation = Quaternion.Euler(dir);
                attack.GetComponent<Projectile>().Pdir = dir;
                break;
        }
    }
}
