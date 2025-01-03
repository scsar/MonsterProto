using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShower : MonoBehaviour
{
    public GameObject Arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(DropArrow());
    }

    private IEnumerator DropArrow()
    {
        for (int i = 0; i <= 3; i++)
        {
            GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);
            Destroy(arrow, 2f);
            arrow.transform.Rotate(0, 0, -90);

            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }
}
