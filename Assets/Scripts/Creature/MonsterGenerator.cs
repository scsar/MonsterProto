using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    // 해당 스크립트를 가진 트리거존을 밟으면, 해당 오브젝트에 저장되어있는 몬스터 활성화. 
    // 이때 몬스터정보를 리스트에 저장하여, 개수와 위치를 제한하지않도록한다.

    [SerializeField]
    private List<GameObject> MonsterList;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MonsterList.Count; i ++)
        {
            MonsterList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        for (int i = 0; i < MonsterList.Count; i ++)
        {
            MonsterList[i].SetActive(true);
        }
    }
}
