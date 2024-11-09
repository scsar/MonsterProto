using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Scriptable Object/Creature", order = int.MaxValue)]
public class CreatureData : ScriptableObject
{

    // 체력
    [SerializeField]
    private float creatureHp;
    public float _creatureHp
    {
        get { return creatureHp; }
    }
    // 해당 크리처가 가질 데미지
    [SerializeField]
    private float creatureDamage;
    public float _creatureDamage
    {
        get { return creatureDamage; }
    }

    [SerializeField]
    private int stageIndex;
    public int _stageIndex
    {
        get { return stageIndex; }
    }


}
