using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damageAmount = 10; // �浹 �� �� ������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹 �߻�");

        if (collision.gameObject.CompareTag("Enemy"))
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
