using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
    // 名前
    [SerializeField]
    string _name = "";
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    // レベル
    [SerializeField]
    int _level = 1;
    public int Level {
        get { return _level; }
        set { _level = value; }
    }
    // 最大HP
    [SerializeField]
    int _maxHp = 10;
    public int MaxHp {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    // HP
    [SerializeField]
    int _hp = 10;
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Max(0, Mathf.Min(MaxHp, value));
        }
    }
    // 力
    [SerializeField]
    int _power = 5;
    public int Power {
        get { return _power; }
        set { _power = value; }
    }
    // 耐久力
    [SerializeField]
    int _strenght = 10;
    public int Strength {
        get { return _strenght; }
        set { _strenght = value; }
    }
    // 素早さ
    [SerializeField]
    int _agility = 5;
    public int Agility {
        get { return _agility; }
        set { _agility = value; }
    }
}