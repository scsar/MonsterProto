using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
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
    private Animator animator;

    private NavMeshAgent agent;
    private Transform target;

    private bool isAttacked;  // 공격중인 상황에 이중으로 공격을 수행하지않도록 제한
    private bool isActive;  // 스킬사용이 완료되기 이전에, 몬스터가 움직이지 않도록 제한

    private float rotateSpeed = 10f;
    private float Hp;
    private float damage;
    //몬스터당 고유 숫자를 기입해 해당 숫자에 해당하는 구간에 들어가 스킬 수행할수있게함
    private int creatureNum;
    private int creatureType;

    
    [HideInInspector]
    public float movedir = -0.5f;
    [HideInInspector]
    public float moveEuler = 0;
    [HideInInspector]
    public bool isMovingRight = true; // 현재 이동 방향
    private Rigidbody2D rb;
    public float speed = 2f;


    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;

        isAttacked = false;
        isActive = false;

        Hp = creatureData._creatureHp;
        damage = creatureData._creatureDamage;
        creatureNum = creatureData._creatureNumber;
        creatureType = creatureData._creatureType;


        
    }

    void Update()
    {
        if (creatureType == 0 && !isActive)
        {
            float moveDirection = isMovingRight ? 1f : -1f;
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        }

    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) < searchDistance && !isActive && creatureType != 0)
        {
            // 이동 Animation 활성화
            animator.SetBool("isSearch", true);
            agent.isStopped = false;
            agent.destination = target.position;
            // 특수한 상황에서 creaturenum을 0으로 설정해 플레이어에게 접근하는기능만 수행하도록한다.
            if (!isAttacked && creatureNum != 0)
            {
                agent.isStopped = true;
                isAttacked = true;
                StartCoroutine(Attack());
            }
            agent.isStopped = false;
            agent.destination = target.position;
        }
        else if (creatureType == 0 && !isActive)
        {
            // creature가 근접공격형 타입일때 공격코드
            if (!isAttacked && creatureNum != 0 && creatureType == 0)
            {
                Debug.Log("근접 공격");
                isAttacked = true;
                StartCoroutine(Attack());
            }
        }
        
        if (Vector2.Distance(transform.position, target.position) < stopDistance || isActive)
        {
            animator.SetBool("isSearch", false);
            agent.isStopped = true;
        }


    }

    void LateUpdate()
    {
        if (creatureType != 0)
        {
            // 몬스터가 플레이어를 바라보는것처럼 할수있도록 위치에 따라 rotation
            Vector2 dir = (target.position - transform.position).normalized;
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
        
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
        animator.SetBool("isAttack", true);
        isActive = true;
        // 스킬을 보유하고있는 스크립트를 불러와 사용후
        GetComponent<CreatureSkill>().ActiveSkill(damage, target, creatureNum);
        yield return new WaitUntil(() => GetComponent<CreatureSkill>().isFinished == true);
        isActive = false;
        animator.SetBool("isAttack", false);
        // 일정시간 대기
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        // 스킬을 다시 사용할수있게 변경
        isAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("isHit");
        Hp -= damage;
        Debug.Log($"Enemy HP: {Hp}");

        if (Hp <= 0)
        {
           StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Debug.Log("Enemy has been defeated!");
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
            {
                collision.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
    }


}
