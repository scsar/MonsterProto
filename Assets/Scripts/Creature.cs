using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private CreatureData creatureData; // 체력, 공격력등 각종 세팅값을 가져올 ScriptableData
    [SerializeField]
    private float stopDistance = 4f; // 해당 변수값 이하로는 플레이어에게 접근하지 않는다.
    [SerializeField]
    private float searchDistance = 20f; // 변수 값 이내에 플레이어가 위치할경우 추적을 시작한다.

    private NavMeshAgent agent;
    private Transform target;

    private bool isAttacked;
    private bool isActive;

    private float Hp;
    private float damage;
    private int stageIdx;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        isAttacked = false;
        isActive = false;

        Hp = creatureData._creatureHp;
        damage = creatureData._creatureDamage;
        stageIdx = creatureData._stageIndex;
        
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) < searchDistance && !isActive)
        {
            agent.isStopped = false;
            agent.destination = target.position;
            if (!isAttacked)
            {
                agent.isStopped = true;
                isAttacked = true;
                StartCoroutine(Attack());
            }
        }
        
        if (Vector2.Distance(transform.position, target.position) < stopDistance)
        {
            agent.isStopped = true;
        }
    }

    void LateUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator Attack()
    {
        isActive = true;
        // 스킬을 보유하고있는 스크립트를 불러와 사용후
        GetComponent<CreatureSkill>().ActiveSkill(damage, target, stageIdx);
        yield return new WaitForSeconds(1f);
        isActive = false;
        // 일정시간 대기
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        // 스킬을 다시 사용할수있게 변경
        isAttacked = false;
        
    }
}
