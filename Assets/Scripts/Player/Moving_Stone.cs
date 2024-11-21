using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Stone : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f; // �̵� �ӵ�
    private float startX = -5f; // ���� ��ġ X
    private float endX = 5f;    // �� ��ġ X

    void FixedUpdate()
    {
        // Mathf.PingPong�� ����Ͽ� -5���� 5 ���̷� �̵�
        float newX = Mathf.PingPong(Time.time * speed, endX - startX) + startX;

        // ���ο� X ��ǥ�� ���� ������Ʈ�� ��ġ�� �ݿ�
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
