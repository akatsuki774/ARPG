using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Weapon", menuName = "CreateWeapon")]
public class Weapon : Item
{
    public enum Type
    {
        Sword,
        Shield
    }
    // 武器タイプ
    [SerializeField]
    Type _type = Type.Sword;
    public Type WeaponType
    {
        get { return _type; }
        set { _type = value; }
    }
    // 増減値
    [SerializeField]
    int _point;
    public int Point{
        get { return _point; }
        set { _point = value; }
    }
}
