using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Creature Parent; 
    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.GetComponent<Creature>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Parent.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log("turn");
            // StartCoroutine(Turn());
            Parent.isGrounded = false;
            Parent.moveEuler += 180f;
            Parent.transform.rotation = Quaternion.Euler(0, Parent.moveEuler, 0);
        }
    }

    private IEnumerator Turn()
    {
        Parent.transform.GetComponent<Creature>().isGrounded = false;
        yield return new WaitForSeconds(1f);
    }
}
