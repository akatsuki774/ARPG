﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharactorObject
{
    [SerializeField]
    CharacterStatus status = null;

    public GameObject CameraObject;

    private Dungueon dungueon;

    // ステータス

    private int currentLevel;
    private Layer2D layer2D;
    private Animator animator;
    private int currentDirection;

    // Start is called before the first frame update
    void Awake() {
        CameraObject = Camera.main.gameObject;
        currentLevel = 1;
        animator = GetComponent<Animator>();
        currentDirection = 1;
        animator.SetInteger( "Direction", currentDirection );
    }

    public void SetDungueon( Dungueon dungueon ) {
        this.dungueon = dungueon;
    }

    public void SetLlayer2D( Layer2D layer2D ) {
        this.layer2D = layer2D;
    }

    // Update is called once per frame

    public void FocusCamera() {
        Camera camera = CameraObject.GetComponent<Camera>();
        CameraObject.transform.position = new Vector3( this.transform.position.x, this.transform.position.y - camera.orthographicSize / 2, -10 );
    }

    bool Action() {
        if ( Input.GetKeyDown( "a" ) ) {
            return Move( 2 );
        } else
        if ( Input.GetKeyDown( "d" ) ) {
            return Move( 0 );
        } else
        if ( Input.GetKeyDown( "w" ) ) {
            return Move( 3 );
        } else
        if ( Input.GetKeyDown( "s" ) ) {
            return Move( 1 );
        } else
        if ( Input.GetKeyDown( "z" ) ) {
            return Attack();
        }

        return false;
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
        if ( dungueon.CheckPassable( pos ) && state != ( int )Layer2D.MapValue.Forbid &&
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

    public int GetCurrentLevel() {
        return currentLevel;
    }

    public override int CalcAttackPoint() {
        return 0;
    }

    public void LevelUp() {
    }
}
