using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : ScriptableObject
{
    // ���x��
    [SerializeField]
    int level;
    // �ő�HP
    [SerializeField]
    int maxHp = 10;
    // HP
    [SerializeField]
    int hp = 10;
    // ��
    [SerializeField]
    int agility = 5;
    // �ϋv��
    [SerializeField]
    int strength = 10;
}
