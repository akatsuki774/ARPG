using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    public int damagePoint = 1;
    public int experience = 1;

    private AStar aster;

    void Awake() {
        aster = new AStar();
        currentLife = maxLife = DefaultLife;
        isAlive = true;
    }

    public void Action( Layer2D layer, Player player ) {
        Vector2 pos = new Vector2( transform.position.x - player.transform.position.x,
            transform.position.y - player.transform.position.y );
        if ( Mathf.Abs( pos.x ) + Mathf.Abs( pos.y ) <= 1 ) {
            Attack( player );
        } else {
            Move( layer, player );
        }
    }

    public int GetExp() {
        return experience;
    }

    void Attack( Charactor target ) {
        this.AttackOnTarget( target );
    }

    void Move( Layer2D layer, Player player ) {
        Vector2 enemyPos = new Vector2( transform.position.x, transform.position.y );
        Vector2 playerPos = new Vector2( player.transform.position.x, player.transform.position.y );

        Vector2 distance = new Vector2( playerPos.x - enemyPos.x, playerPos.y - enemyPos.y );

        Vector2 movePos;
        //if ( Mathf.Abs( distance.x ) + Mathf.Abs( distance.y ) < 10 ) {
        //    movePos = aster.CalcWay( layer, enemyPos, playerPos, false );
        //} else {
            movePos = new Vector2( enemyPos.x, enemyPos.y );
            switch ( Random.Range( 0, 3 ) ) {
                case 0:
                    movePos.x += 1;
                    break;
                case 1:
                    movePos.y += 1;
                    break;
                case 2:
                    movePos.y -= 1;
                    break;
                case 3:
                    movePos.x -= 1;
                    break;
            }
        //}

        int state = layer.Get( ( int )movePos.x, ( int )movePos.y );
        if ( state == ( int )Layer2D.MapValue.Pass ) {
            transform.gameObject.transform.position = new Vector3( movePos.x, movePos.y, 0 );
            layer.Set( ( int )enemyPos.x, ( int )enemyPos.y, ( int )Layer2D.MapValue.Pass );
            layer.Set( ( int )movePos.x, ( int )movePos.y, ( int )Layer2D.MapValue.Enemy );
        }
    }

    public override int GetDamage() {
        return damagePoint;
    }
}
