using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Vector2Int startPos;
    [SerializeField]
    Vector2Int mapSize;
    [SerializeField]
    GameObject dotPrefab;

    private GameObject[,] objArray;
    private Layer2D layer2D;

    void Awake()
    {
        objArray = new GameObject[mapSize.y, mapSize.x];
        int dotSize = ( int )dotPrefab.GetComponent<RectTransform>().sizeDelta.x;

        for ( int i = 0; i < mapSize.y; i++ )
        {
            for ( int j = 0; j < mapSize.x; j++ )
            {
                GameObject obj = Instantiate(dotPrefab, this.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos.x + dotSize * j, startPos.y + dotSize * i, 0);
                objArray[i, j] = obj;
                // obj.SetActive(false);
            }
        }
    }

    public void SetLayer2D( Layer2D layer)
    {
        layer2D = layer;
    }

    public void UpdateMap( Vector2Int playerPos )
    {
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                switch( ( Layer2D.MapValue )layer2D.Get( playerPos.x - mapSize.x / 2 + j, playerPos.y - mapSize.y / 2 + i) )
                {
                    case Layer2D.MapValue.Pass:
                        objArray[i, j].SetActive(true);
                        objArray[i, j].GetComponent<Image>().color = Color.blue;
                        break;
                    case Layer2D.MapValue.Enemy:
                        objArray[i, j].SetActive(true);
                        objArray[i, j].GetComponent<Image>().color = Color.red;
                        break;
                    case Layer2D.MapValue.Stair:
                        objArray[i, j].SetActive(true);
                        objArray[i, j].GetComponent<Image>().color = Color.black;
                        break;
                    default:
                        objArray[i, j].SetActive(false);
                        break;
                }
            }
        }

        // プレイヤーを中心に描画する
        objArray[mapSize.y / 2, mapSize.x / 2].SetActive(true);
        objArray[mapSize.y / 2, mapSize.x / 2].GetComponent<Image>().color = Color.white;
    }
}
