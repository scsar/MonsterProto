using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Stone : MonoBehaviour
{
    public float speed = 2f;  // �̵� �ӵ�
    private Vector3 startPosition;  // ������Ʈ�� �ʱ� ��ġ
    private float range = 5f;  // �̵� ����

    void Start()
    {
        // �ʱ� ��ġ�� ����
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // �ʱ� ��ġ�� �������� -5���� +5 ���̷� �̵�
        float newX = Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.position = new Vector3(startPosition.x + newX, transform.position.y, transform.position.z);
    }
}
