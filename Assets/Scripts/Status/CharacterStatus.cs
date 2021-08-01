using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : ScriptableObject
{
    // レベル
    [SerializeField]
    int level;
    // 最大HP
    [SerializeField]
    int maxHp = 10;
    // HP
    [SerializeField]
    int hp = 10;
    // 力
    [SerializeField]
    int agility = 5;
    // 耐久力
    [SerializeField]
    int strength = 10;
}
