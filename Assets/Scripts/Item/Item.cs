using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string _name = "";
    public string Name
    {
        get { return _name; }
    }

    [SerializeField]
    private Sprite _icon = null;
    public Sprite Icon
    {
        get { return _icon; }
    }

    [SerializeField]
    private string _detail = "";
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
