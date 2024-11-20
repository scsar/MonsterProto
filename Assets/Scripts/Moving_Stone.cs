using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Stone : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f; // 이동 속도
    private float startX = -5f; // 시작 위치 X
    private float endX = 5f;    // 끝 위치 X

    void FixedUpdate()
    {
        // Mathf.PingPong을 사용하여 -5에서 5 사이로 이동
        float newX = Mathf.PingPong(Time.time * speed, endX - startX) + startX;

        // 새로운 X 좌표를 게임 오브젝트의 위치에 반영
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
