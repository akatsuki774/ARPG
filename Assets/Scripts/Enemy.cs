using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterObject
{
    public int damagePoint = 1;
    public int experience = 1;

    private AStar aster;

    void Awake() {
        aster = new AStar();
    }

    public void Action( Layer2D layer, Dictionary<Vector2Int, CharacterObject> characterObjects, Player player ) {
        Vector2 pos = new Vector2( transform.position.x - player.transform.position.x,
            transform.position.y - player.transform.position.y );
        if ( Mathf.Abs( pos.x ) + Mathf.Abs( pos.y ) <= 1 ) {
            Attack( player );
        } else {
            Move( layer, characterObjects, player );
        }
    }

    public int GetExp() {
        return experience;
    }

    void Attack( CharacterObject target ) {
        this.AttackOnTarget( target );
    }

    void Move( Layer2D layer, Dictionary<Vector2Int, CharacterObject> characterObjects, Player player ) {
        Vector2Int enemyPos = this.GetPosition();
        Vector2Int playerPos = new Vector2Int( ( int )player.transform.position.x, ( int )player.transform.position.y );

        Vector2Int distance = new Vector2Int( playerPos.x - enemyPos.x, playerPos.y - enemyPos.y );

        Vector2Int movePos;
        if ( Mathf.Abs( distance.x ) + Mathf.Abs( distance.y ) < 10 )
        {
            movePos = aster.CalcWay( layer, enemyPos, playerPos, false );
        }
        else
        {
            movePos = new Vector2Int( enemyPos.x, enemyPos.y );
            switch ( Random.Range( 0, 3 ) )
            {
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
        }

        int state = layer.Get( movePos.x, movePos.y );
        if ( state == ( int )Layer2D.MapValue.Pass && !characterObjects.TryGetValue( movePos, out _ ) ) {
            characterObjects.Remove( this.GetPosition() );
            transform.position = new Vector3( movePos.x, movePos.y, 0 );
            characterObjects.Add( movePos, this );
        }
    }

    public override int CalcAttackPoint() {
        return damagePoint;
    }
}
