using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Pdir;

    private float Pspeed = 5f;
    void Start()
    {
        Destroy(gameObject, 5f);
        //TODO
        // 발사하는 프리펩의 기본 설정값에 따라서 어떤건 정상형태로 나가고 어떤건 뒤집혀서 나감
        // 설정필요
        if (Pdir.x > 0)
        {
            Pdir.x = -Pdir.x;
            transform.rotation = Quaternion.Euler(0, -180, Pdir.x);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, Pdir.x);
        }        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Pdir * Pspeed * Time.deltaTime);
    }

  
}
