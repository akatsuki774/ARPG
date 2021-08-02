using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorObject : MonoBehaviour
{
    protected int _currentHp;
    protected bool _isAlive;

    public abstract int CalcAttackPoint();

    public void AttackOnTarget( CharactorObject target ) {
        target._currentHp -= this.CalcAttackPoint();
        if ( target._currentHp <= 0 ) {
            target._currentHp = 0;
            target._isAlive = false;
        }
    }
    public bool IsAlive {
        get { return _isAlive; }
        set { _isAlive = value; }
    }


    public int CurrentHp {
        get { return _currentHp; }
        set { _currentHp = value; }
    }
}
