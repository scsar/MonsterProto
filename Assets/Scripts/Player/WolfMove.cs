using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class WolfMove : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 이 스크립트가 적용된 오브젝트의 SpriteRenderer
    private Collider2D Wolf_Col; // 이 스크립트가 적용된 오브젝트의 Collider2D\    private Animator anim;
    private Animator anim;

    public float moveDistance = 10f; // Ctrl 키를 누를 때 이동할 거리
    public float Wolf_moveSpeed = 60f; // 마우스 커서 방향으로 이동할 속도

    private bool isMovingTowardsCursor = false;
    private bool isTriggerEnabled = true;
    public GameObject ctrl_Attack; // 활성화할 게임 오브젝트
    public GameObject q_Attack;
    public GameObject DontMove;

    private float cooltime = 10f;
    private float cooltime_max = 10f;

    
    public Image[] disable;
    bool cool1 = true;
    bool cool2 = true;



    void Start()
    {
        cool1 = true;
        cool2 = true;
        disable[0].fillAmount = 0;
        disable[1].fillAmount = 0;

        // 컴포넌트를 Start에서 자동으로 할당
        isTriggerEnabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Wolf_Col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (!isMovingTowardsCursor)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                if (ctrl_Attack != null && cool1 && Wolf_Col.isTrigger)
                {
                    StartCoroutine(Basic_attack());
                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && cool2 && Wolf_Col.isTrigger)
            {
                if (q_Attack != null)
                {
                    StartCoroutine(Crying_attack());
                }
            }
            if (Input.GetMouseButtonDown(0) ) // 이미 이동 중이 아닌 경우에만 시작
            {
                if (!Wolf_Col.isTrigger)
                {
                    StartCoroutine(ShowText());
                }
                else if (isTriggerEnabled)
                {
                    StartCoroutine(MoveTowardsCursor());
                }

                    
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (isTriggerEnabled)
                {
                    anim.SetBool("fix", true);
                    spriteRenderer.color = Color.gray;
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    Wolf_Col.isTrigger = false;
                    isTriggerEnabled = false;
                    
                }
                else
                {
                    anim.SetBool("fix", false);
                    spriteRenderer.color = Color.white;
                    Wolf_Col.isTrigger = true;
                    isTriggerEnabled = true;
                }
            }
        }
    }
    IEnumerator Basic_attack()
    {
        
        anim.SetBool("attack", true);
        ctrl_Attack.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ctrl_Attack.SetActive(false);
        anim.SetBool("attack", false);
        yield return StartCoroutine(CoolTimeFunc());

    }
    IEnumerator Crying_attack()
    {
        anim.SetBool("crying", true);
        q_Attack.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        q_Attack.SetActive(false);
        anim.SetBool("crying", false);
        yield return StartCoroutine(CoolTimeFunc2());

    }
    IEnumerator MoveTowardsCursor()
    {
        anim.SetBool("move", true);

        isMovingTowardsCursor = true;

        // 마우스 커서의 월드 좌표 가져오기 (2D 기준)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // z값을 0으로 설정하여 2D 공간에서 사용

        // SpriteRenderer를 통해 스프라이트의 방향만 전환
        if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // 목표 지점까지 지속적으로 이동
        while (Vector2.Distance(transform.position, mousePosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, Wolf_moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        isMovingTowardsCursor = false;
        anim.SetBool("move", false);

    }

    IEnumerator ShowText()
    {
        DontMove.SetActive(true);
        yield return new WaitForSeconds(1f);
        DontMove.SetActive(false);
    }

    IEnumerator CoolTimeFunc() {
        cool1 = false;
        disable[0].fillAmount = 1;
        cooltime = 0.5f;
        while(cooltime > 0.0f)
        {
            cooltime -= Time.deltaTime;

            disable[0].fillAmount = cooltime / 0.5f;
            yield return null;
        }
        cool1 = true;
    }

    IEnumerator CoolTimeFunc2()
    {
        cool2 = false;
        disable[1].fillAmount = 1;
        cooltime = 3;
        while (cooltime > 0.0f)
        {
            cooltime -= Time.deltaTime;

            disable[1].fillAmount = cooltime /3;
            yield return null;
        }
        cool2 = true;

    }
}