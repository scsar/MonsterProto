using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class LeafBoss : MonoBehaviour
{
    public GameObject[] moveandShootPosition;
    public GameObject Droparrow;
    public GameObject Shootarrow;
    public GameObject linePrefeb;
    public Transform ShootPosition;
    


    public IEnumerator LeafSkill(Transform target)
    {
        int skillnum = Random.Range(1, 4);
        switch(skillnum)
        {
            case 1:
            yield return StartCoroutine(ArrowShower());
            break;
            case 2:
            yield return StartCoroutine(ShootArrow());
            break;
            case 3:
            yield return StartCoroutine(ArrowSpread(target));
            break;

        }


        
    }

    private IEnumerator ArrowShower()
    {
        for (int i = 0; i <= 6;)
        {
            int rand = Random.Range(0, 10);
            if (transform.GetChild(rand).gameObject.activeSelf == false)
            {
                transform.GetChild(rand).gameObject.SetActive(true);
                i++;
            }
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= 9; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private IEnumerator ShootArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject shootarrow = Instantiate(Shootarrow);
            shootarrow.transform.position = ShootPosition.position;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ArrowSpread(Transform target)
    {
        int rand = Random.Range(0, 4);
        transform.position = moveandShootPosition[rand].transform.position;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        List<GameObject> lines = new List<GameObject>();
        for (float i = 0; i < 36; i+= 1)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject line = Instantiate(linePrefeb);

            line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 60f + (15f * i)));
            line.transform.SetParent(transform);
            line.transform.localPosition = new Vector3(0.2f, -0.5f, 0);
            lines.Add(line);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        // 리스트에 저장한 라인 각각 꺼내서 pop하면서 발사.
        for (int i = 0; i < 36; i++)
        {
            float lineAngle = (lines[i].transform.eulerAngles.z - 90f) * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(lineAngle), Mathf.Sin(lineAngle), 0);
            
            GameObject attack = Instantiate(Droparrow);
            attack.transform.position = lines[i].transform.position;
            attack.GetComponent<DropArrow>().dir = direction;
            Destroy(lines[i]);
        }

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}