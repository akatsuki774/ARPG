using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterObject
{
    // プレイヤーステータス
    [SerializeField]
    private AllyStatus status = null;

    Camera mainCamera;
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

    // Update is called once per frame

    public void FocusCamera() {
        mainCamera.transform.position = new Vector3( this.transform.position.x, this.transform.position.y - mainCamera.orthographicSize / 2, -10 );
    }

    public bool Move( Layer2D layer, Dictionary<Vector2Int, CharacterObject> characterObjects, int direction ) {

        animator.SetInteger( "Direction", direction );
        if ( currentDirection != direction ) {
            currentDirection = direction;
            return false;
        }

        Vector2Int movePos = this.GetPosition();
        switch( direction ) {
            case 0:
                movePos.x += 1;
                break;
            case 1:
                movePos.y += -1;
                break;
            case 2:
                movePos.x += -1;
                break;
            case 3:
                movePos.y += 1;
                break;
        }

        int state = layer.Get( movePos.x, movePos.y );
        if ( state != ( int )Layer2D.MapValue.Forbid && !characterObjects.TryGetValue( movePos, out _ ) ) {
            characterObjects.Remove( this.GetPosition() );
            transform.position = new Vector3( movePos.x, movePos.y, 0 );
            characterObjects.Add( movePos, this );
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
