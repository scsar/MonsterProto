using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossSkill : MonoBehaviour
{
    [HideInInspector]
    public bool isFinished;
    private LeafBoss leaf;

    void Start()
    {
        leaf = GetComponent<LeafBoss>();
    }

    public void ActiveSkill(Transform target, int creatureNum)
    {
        isFinished = false;
        switch(creatureNum)
        {
            case 7:
            StartCoroutine(ExcuteLeafSkill(target));
            break;
        }
        
    } 

    private IEnumerator ExcuteLeafSkill(Transform target)
    {
        if (!isFinished)
        {
            yield return StartCoroutine(leaf.LeafSkill(target)); // LeafSkill 완료 대기
            isFinished = true;
        }
    }
}

