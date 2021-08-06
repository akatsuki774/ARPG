using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : MonoBehaviour
{
    protected int _currentHp;
    protected bool _isAlive;

    public abstract int CalcAttackPoint();

    public void AttackOnTarget( CharacterObject target ) {
        target._currentHp -= this.CalcAttackPoint();
        if ( target._currentHp <= 0 ) {
            target._currentHp = 0;
            target._isAlive = false;
        }
    }
    public bool IsAlive {
        get { return _isAlive; }
    }

    public int CurrentHp {
        get { return _currentHp; }
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int( ( int )transform.position.x, ( int )transform.position.y );
    }
}
