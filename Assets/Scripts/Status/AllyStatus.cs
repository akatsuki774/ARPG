using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AllyStatus", menuName = "CreateAllyStatus")]
public class AllyStatus : CharacterStatus
{
    // 経験値
    [SerializeField]
    int _experiencePoint = 0;
    public int ExperiencePoint {
        get { return _experiencePoint; }
        set { _experiencePoint = value; }
    }
    // 左手装備
    [SerializeField]
    Weapon _leftWeapon = null;
    public Weapon LeftWeapon {
        get { return _leftWeapon; }
        set { _leftWeapon = value; }
    }
    // 右手装備
    [SerializeField]
    Weapon _rightWeapon = null;
    public Weapon RightWeapon {
        get { return _rightWeapon; }
        set { _rightWeapon = value; }
    }
}
