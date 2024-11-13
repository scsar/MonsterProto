using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceLine : MonoBehaviour
{
    private bool isTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTouched)
        {
            transform.localScale += new Vector3(0, 1f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isTouched = true;
        }
    }
}
