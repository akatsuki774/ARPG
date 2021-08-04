using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharactorObject
{
    // プレイヤーステータス
    [SerializeField]
    private AllyStatus status = null;

    Camera mainCamera;
    Layer2D layer2D;
    Animator animator;
    int currentDirection;

    // Start is called before the first frame update
    void Awake() {
        animator = GetComponent<Animator>();
        currentDirection = 1;
        animator.SetInteger( "Direction", currentDirection );
    }

    public void Init( Camera camera )
    {
        mainCamera = camera;
    }

    public void SetLlayer2D( Layer2D layer2D ) {
        this.layer2D = layer2D;
    }

    // Update is called once per frame

    public void FocusCamera() {
        mainCamera.transform.position = new Vector3( this.transform.position.x, this.transform.position.y - mainCamera.orthographicSize / 2, -10 );
    }

    public bool Move( int direction ) {

        animator.SetInteger( "Direction", direction );
        if ( currentDirection != direction ) {
            currentDirection = direction;
            return false;
        }

        Vector2 pos = new Vector2( transform.position.x, transform.position.y );
        switch( direction ) {
            case 0:
                pos.x += 1;
                break;
            case 1:
                pos.y += -1;
                break;
            case 2:
                pos.x += -1;
                break;
            case 3:
                pos.y += 1;
                break;
        }

        int state = layer2D.Get( ( int )pos.x, ( int )pos.y );
        if ( state != ( int )Layer2D.MapValue.Forbid &&
             state != ( int )Layer2D.MapValue.Enemy ) {
            transform.position = new Vector3( pos.x, pos.y, 0 );
            FocusCamera();
            return true;
        }

        return false;
    }

    public bool Attack() {
        Vector2 pos = new Vector2( transform.position.x, transform.position.y );
        switch ( currentDirection ) {
            case 0:
                pos.x += 1;
                break;
            case 1:
                pos.y += -1;
                break;
            case 2:
                pos.x += -1;
                break;
            case 3:
                pos.y += 1;
                break;
        }
        GameManager.instance.AttackOnTarget( pos );
        return true;
    }

    public override int CalcAttackPoint() {
        return 0;
    }

    public void LevelUp() {
    }

    public Weapon GetLeftWeapon()
    {
        return status.LeftWeapon;
    }

    public Weapon GetRightWeapon()
    {
        return status.RightWeapon;
    }
}
