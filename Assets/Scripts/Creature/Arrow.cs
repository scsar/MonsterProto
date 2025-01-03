using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform target;
    [HideInInspector]
    public Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        dir = (target.position - transform.position).normalized;
        if (dir.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-3, 3, 1);
        }
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Rigidbody2D>().AddForce(dir * 0.3f, ForceMode2D.Impulse);
        transform.Translate(dir * 10f * Time.deltaTime);
    }
}
