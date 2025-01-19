using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Creature parent; 
    public float groundCheckDistance = 5f; // 지면 감지 거리

    void Start()
    {
        parent = transform.parent.GetComponent<Creature>();
    }

    void Update()
    {
        Vector2 origin = transform.position;
        RaycastHit2D groundInfo = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance);

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.red); // 디버그용 Ray 그리기
        // 지면 체크
        if (!groundInfo.collider) 
        {
            Flip();
        }
        Debug.Log(groundInfo.collider);
    }

    private void Flip()
    {
        parent.isMovingRight = !parent.isMovingRight; // 이동 방향 반전
        parent.transform.localScale = new Vector3(-parent.transform.localScale.x, parent.transform.localScale.y, parent.transform.localScale.z);
    }
}
