using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMove : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // �� ��ũ��Ʈ�� ����� ������Ʈ�� SpriteRenderer
    private Collider2D Wolf_Col; // �� ��ũ��Ʈ�� ����� ������Ʈ�� Collider2D\    private Animator anim;
    private Animator anim;

    public float moveDistance = 10f; // Ctrl Ű�� ���� �� �̵��� �Ÿ�
    public float Wolf_moveSpeed = 60f; // ���콺 Ŀ�� �������� �̵��� �ӵ�

    private bool isMovingTowardsCursor = false;
    private bool isTriggerEnabled = true;
    public GameObject ctrl_Attack; // Ȱ��ȭ�� ���� ������Ʈ

    public GameObject DontMove;

    void Start()
    {
        // ������Ʈ�� Start���� �ڵ����� �Ҵ�
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
                if (ctrl_Attack != null)
                {
                    StartCoroutine(Basic_attack());
                }
            }

            if (Input.GetMouseButtonDown(0) ) // �̹� �̵� ���� �ƴ� ��쿡�� ����
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
                    //Debug.Log("����");
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    Wolf_Col.isTrigger = false;
                    isTriggerEnabled = false;
                    
                }
                else
                {
                    anim.SetBool("fix", false);

                    spriteRenderer.color = Color.white;
                    //Debug.Log("����");
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

    }

    IEnumerator MoveTowardsCursor()
    {
        anim.SetBool("move", true);

        isMovingTowardsCursor = true;

        // ���콺 Ŀ���� ���� ��ǥ �������� (2D ����)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // z���� 0���� �����Ͽ� 2D �������� ���

        // SpriteRenderer�� ���� ��������Ʈ�� ���⸸ ��ȯ
        if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // ��ǥ �������� ���������� �̵�
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
        yield return new WaitForSeconds(0.5f);
        DontMove.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        DontMove.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        DontMove.SetActive(false);
    }
}