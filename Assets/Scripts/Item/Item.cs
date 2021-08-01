using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        HPRecovery,
        Weapon,
        Armor,
    }

    [SerializeField]
    Type _itemType = Type.HPRecovery;
    public Type ItemType
    {
        get { return _itemType; }
    }

    [SerializeField]
    string _name = "";
    public string Name
    {
        get { return _name; }
    }

    [SerializeField]
    string _sortName = "";
    public string SortName
    {
        get { return _sortName; }
    }

    [SerializeField]
    string _detail = "";
    public string Detail
    {
        get { return _detail; }
    }

    [SerializeField]
    int _amount = 0;
    public int Amount
    {
        get { return _amount; }
    }
}
