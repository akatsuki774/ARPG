using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public void OnClickButton( int value ) {
        GameManager.instance.PlayerTurn( value );
    }
}
