using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private Item item;

    public Item Item
    {
        get { return item; }
        set
        {
            this.item = value;
            this.GetComponent<SpriteRenderer>().sprite = value.Icon;
        }
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int( ( int )transform.position.x, ( int )transform.position.y );
    }
}
