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

    private float rotateSpeed = 10f;
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
        Vector2 dir = (target.position - transform.position).normalized;
        // LookPlayer();
        if (dir.x > 0)
        {
            dir.x = -dir.x;
            transform.rotation = Quaternion.Euler(0, -180, dir.x);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, dir.x);
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


    // 부정확해서 사용보류
    void LookPlayer()
    {
        if (target != null) 
        {
            Vector2 direction = new Vector2
            (
                transform.position.x - target.position.x,
                transform.position.y - target.position.y
            );
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);

        // 현재 X축 회전을 유지하면서 Z축만 클램핑
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float clampedZRotation = currentRotation.z;

        // Z축 회전 값을 70도 이내로 제한
        if (currentRotation.z > 70f && currentRotation.z <= 180f)
        {
            clampedZRotation = 70f;
        }
        else if (currentRotation.z < 290f && currentRotation.z > 180f)
        {
            clampedZRotation = 290f;
        }

        // Z축 제한된 각도를 적용하면서 Y축 반전 조건 설정
        float yRotation = (clampedZRotation == 70f || clampedZRotation == 290f) ? 180f : 0f;

        // 최종 회전 값 적용 (X축은 기존 값 유지, Y와 Z만 설정)
        transform.rotation = Quaternion.Euler(currentRotation.x, yRotation, clampedZRotation);
        }
    }
}
