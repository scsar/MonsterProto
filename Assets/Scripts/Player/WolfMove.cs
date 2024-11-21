using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMove : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // �� ��ũ��Ʈ�� ����� ������Ʈ�� SpriteRenderer
    private Collider2D Wolf_Col; // �� ��ũ��Ʈ�� ����� ������Ʈ�� Collider2D
    public float moveDistance = 10f; // Ctrl Ű�� ���� �� �̵��� �Ÿ�
    public float Wolf_moveSpeed = 30f; // ���콺 Ŀ�� �������� �̵��� �ӵ�

    private bool isMovingTowardsCursor = false;
    private bool isTriggerEnabled = true;

    public GameObject ctrl_Attack; // Ȱ��ȭ�� ���� ������Ʈ

    void Start()
    {
        // ������Ʈ�� Start���� �ڵ����� �Ҵ�
        isTriggerEnabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Wolf_Col = GetComponent<Collider2D>();
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

            if (Input.GetMouseButtonDown(0) && isTriggerEnabled) // �̹� �̵� ���� �ƴ� ��쿡�� ����
            {
                StartCoroutine(MoveTowardsCursor());
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (isTriggerEnabled)
                {
                    spriteRenderer.color = Color.gray;
                    Debug.Log("����");
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    Wolf_Col.isTrigger = false;
                    isTriggerEnabled = false;
                    
                }
                else
                {
                    spriteRenderer.color = Color.white;
                    Debug.Log("����");
                    Wolf_Col.isTrigger = true;
                    isTriggerEnabled = true;
                }
            }
        }
    }
    IEnumerator Basic_attack()
    {
        ctrl_Attack.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ctrl_Attack.SetActive(false);
    }

    IEnumerator MoveTowardsCursor()
    {
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

        yield return new WaitForSeconds(0.3f);
        isMovingTowardsCursor = false;
    }
}