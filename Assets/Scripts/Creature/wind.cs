using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    [SerializeField]
    private float windForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, windForce), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
