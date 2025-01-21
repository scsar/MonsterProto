using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Stone : MonoBehaviour
{
    public float speed = 2f;  // 이동 속도
    private Vector3 startPosition;  // 오브젝트의 초기 위치
    private float range = 5f;  // 이동 범위

    void Start()
    {
        // 초기 위치를 저장
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // 초기 위치를 기준으로 -5에서 +5 사이로 이동
        float newX = Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.position = new Vector3(startPosition.x + newX, transform.position.y, transform.position.z);
    }
}
