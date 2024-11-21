using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceLine : MonoBehaviour
{
    private bool isTouched = false;

    private float stopTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTime > 1f)
        {
            isTouched = true;
        }
        else
        {
            stopTime += Time.deltaTime;
        }

        if (!isTouched)
        {
            transform.localScale += new Vector3(0, 2f, 0);
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
