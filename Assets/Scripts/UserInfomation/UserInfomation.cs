using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UserInfomation", menuName = "CreateUserInfomation")]
public class UserInfomation : ScriptableObject
{
    // 所持金
    [SerializeField]
    int _money;
    public int Money {
        get { return _money; }
        set { _money = value; }
    }
    // 所持アイテム
    [SerializeField]
    ItemDictionary _itemDictionary;
    public ItemDictionary CurrentItemDictionary {
        get { return _itemDictionary; }
        set { _itemDictionary = value; }
    }

    // アイテムの追加
    public void AddItem( Item item, int num )
    {
        _itemDictionary.Add(item, num);
    }
    //　平仮名の名前でソートしたItemDictionaryを返す
    public IOrderedEnumerable<KeyValuePair<Item, int>> GetSortItemDictionary()
    {
        return _itemDictionary.OrderBy( item => item.Key.Name );
    }
    // アイテムの個数を設定する
    public void SetItemNum( Item item, int num)
    {
        _itemDictionary[item] = num;
    }
    // アイテム数を返す
    public int GetItemNum( Item item)
    {
    return _itemDictionary[item];
    }
}
