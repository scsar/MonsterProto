using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damageAmount = 10; // 충돌 시 줄 데미지

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
        Debug.Log("충돌 발생");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("공격 성공");
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);

            // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            // if (enemy != null)
            // {
            //     enemy.TakeDamage(damageAmount); // Enemy의 HP 감소
            // }
        }
    }

}
