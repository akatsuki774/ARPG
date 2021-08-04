using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ema : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private void Awake()
    {
        image.gameObject.SetActive(false);
    }
    public void DisplayWeapon( Weapon weapon )
    {
        if (weapon is null || weapon.Icon is null)
        {
            image.gameObject.SetActive(false);
        }
        else
        {
            image.sprite = weapon.Icon;
            image.gameObject.SetActive(true);
        }
    }
}
