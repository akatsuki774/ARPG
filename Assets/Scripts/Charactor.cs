using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour
{
    public int DefaultLife;

    protected int currentLife;
    protected int maxLife;
    protected bool isAlive;

    protected virtual void Awake() {
        currentLife = maxLife = DefaultLife;
    }
    public abstract int GetDamage();

    public void AttackOnTarget( Charactor target ) {
        target.currentLife -= this.GetDamage();
        if ( target.currentLife <= 0 ) {
            target.currentLife = 0;
            target.isAlive = false;
        }
    }
    public bool IsAlive() {
        return isAlive;
    }


    public int GetCurrentLife() {
        return currentLife;
    }

    public int GetMaxLife() {
        return maxLife;
    }
}
