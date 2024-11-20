using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    public int HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"Enemy HP: {HP}");

        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has been defeated!");
        Destroy(gameObject); // Enemy ������Ʈ �ı�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹 �߻�");

        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("���� ����");
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);

            // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            // if (enemy != null)
            // {
            //     enemy.TakeDamage(damageAmount); // Enemy�� HP ����
            // }
        }
    }
}
