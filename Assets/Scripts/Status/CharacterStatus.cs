using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : ScriptableObject
{
    // ƒŒƒxƒ‹
    [SerializeField]
    int level;
    // Å‘åHP
    [SerializeField]
    int maxHp = 10;
    // HP
    [SerializeField]
    int hp = 10;
    // —Í
    [SerializeField]
    int agility = 5;
    // ‘Ï‹v—Í
    [SerializeField]
    int strength = 10;
}
