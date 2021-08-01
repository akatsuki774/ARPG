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
    Type ItemType { get; } = Type.HPRecovery;

    [SerializeField]
    string Name { get; } = "";

    [SerializeField]
    string SortName { get; } = "";

    [SerializeField]
    string Detail { get; } = "";

    [SerializeField]
    int Amount { get; } = 0;
}
