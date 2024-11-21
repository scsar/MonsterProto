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
    // 해당 크리처의 고유 숫자(이숫자로 사용할 스킬 지정)
    [SerializeField]
    private int creatureNumber;
    public int _creatureNumber
    {
        get { return creatureNumber; }
    }


}
