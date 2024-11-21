using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    private Collider2D range;
    [SerializeField]
    private GameObject visualPrefeb;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<Collider2D>();
        range.enabled = false;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        range.enabled = true;
        GameObject visual = Instantiate(visualPrefeb,transform.position, Quaternion.identity);
        visual.transform.Translate(0, 0.4f, 0);
        yield return new WaitForSeconds(0.3f);
        Destroy(visual);
        Destroy(gameObject);
    }
}
