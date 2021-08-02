using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
    // レベル
    [SerializeField]
    int Level { get; set; } = 1;
    // HP
    [SerializeField]
    int MaxHp { get; set; } = 10;
    // 力
    [SerializeField]
    int Power { get; set; } = 5;
    // 耐久力
    [SerializeField]
    int Strength { get; set; } = 10;
    // 素早さ
    [SerializeField]
    int Agility { get; set; } = 5;
}
