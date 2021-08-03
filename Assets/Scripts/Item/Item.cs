using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [SerializeField]
    string _name = "";
    public string Name
    {
        get { return _name; }
    }

    [SerializeField]
    string _detail = "";
    public string Detail
    {
        get { return _detail; }
    }

    [SerializeField]
    int _basicPrice = 0;
    public int BasicPrice
    {
        get { return _basicPrice; }
    }
}
