using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArrow : MonoBehaviour
{
    [HideInInspector]
    public Vector2 dir = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dir != Vector2.zero)
        {
            transform.Translate(dir * 10f * Time.deltaTime);
        }
    }
}
