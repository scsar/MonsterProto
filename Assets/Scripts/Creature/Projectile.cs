using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Pdir;

    private float Pspeed = 5f;
    void Start()
    {
        Destroy(gameObject, 5f);
        //TODO
        // 발사하는 프리펩의 기본 설정값에 따라서 어떤건 정상형태로 나가고 어떤건 뒤집혀서 나감
        // 설정필요
        if (Pdir.z > 0)
        {
            Pdir.z = -Pdir.z;
            transform.rotation = Quaternion.Euler(0, 0, Pdir.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, Pdir.z);
        }        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Pdir * Pspeed * Time.deltaTime, Space.World);
    }

  
}
