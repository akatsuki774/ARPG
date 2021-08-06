﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MatrixRange = DTL.Base.Coordinate2DimensionalAndLength2Dimensional;

public class GameManager : MonoBehaviour
{
    public Dungueon dungueon;
    public GameObject playerPrefab;
    public Text lifeText;
    public Text levelText;
    public Text floorText;
    public GameObject enemyPrefab;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private int maxEnemySpawnNum = 10;
    [SerializeField]
    private MiniMap miniMap;
    [SerializeField]
    private Ema leftEma;
    [SerializeField]
    private Ema rightEma;

    public static GameManager instance = null;
    public int spawnTurnSpan = 5;
    public HelthBar helthBar;

    private Player player;
    private List<Enemy> enemyList = new List<Enemy>();

    private Layer2D playerLayer2D;
    // 階段を通過不可にするためLayer2Dを分けている
    private Layer2D enemyLayer2D;
    // マップにあるオブジェクト情報
    private Dictionary<Vector2Int, CharacterObject> characterObjects;

    private int turnCount;
    private int currentFloor;
    private bool isPlayerTurn;

    void Awake() {
        if( instance == null ) {
            instance = this;
        } else {
            Destroy( this.gameObject );
        }
        DontDestroyOnLoad( this.gameObject );
    }
    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        StartGame();
    }

    void Update() {
        if( isPlayerTurn ) {
            return;
        }
        StartCoroutine( EnemyTurn() );
    }

    void InitGame() {
        turnCount = 1;
        currentFloor = 1;
        DispFloor();
        player = Instantiate( playerPrefab, new Vector3( 0, 0, 0 ), Quaternion.identity ).GetComponent<Player>();
        helthBar.SetMaxHelth( 0 );
        DispPlayerStatus();
    }

    void CreateFloor() {
        characterObjects = new Dictionary<Vector2Int, CharacterObject>();
        isPlayerTurn = true;
        dungueon.Create( 64, 64 );
        playerLayer2D = dungueon.ConvertToLayer2D();
        enemyLayer2D = dungueon.ConvertToLayer2D( true );
        Vector2Int pos = new Vector2Int();
        dungueon.GetRandomPosition(ref pos);
        player.transform.position = new Vector3(pos.x, pos.y, 0);
        player.Init(mainCamera);
        player.FocusCamera();
        characterObjects.Add( player.GetPosition(), player );
        miniMap.SetLayer2D( playerLayer2D );
        for ( int i = 0; i < 10; i++ )
        {
            SpawnEnemy();
        }
    }

    void StartGame() {
        CreateFloor();
    }
    void SpawnEnemy() {
        MatrixRange range = new MatrixRange();
        // プレイヤーの周辺にスポーンしないためのエリア範囲
        range.x = ( int )player.transform.position.x - 4;
        range.y = ( int )player.transform.position.y - 4;
        range.w = 9;
        range.h = 9;

        Vector2Int pos = new Vector2Int();
        while ( true )
        {
            dungueon.GetRandomPosition( ref pos, range );
            if( !characterObjects.TryGetValue(pos, out _ ) )
            {
                break;
            }
        }
        Enemy enemy = Instantiate( enemyPrefab, new Vector3( pos.x, pos.y, 0 ), Quaternion.identity ).GetComponent<Enemy>();
        enemyList.Add( enemy );
        characterObjects.Add( enemy.GetPosition(), enemy );
    }

    IEnumerator EnemyTurn() {
        if ( enemyList.Count <= 0 ) {
            NextTurn();
            yield break;
        }

        foreach ( var enemy in enemyList ) {
            enemy.Action( enemyLayer2D, characterObjects, player );
        }

        NextTurn();
        yield return 0;
    }

    void NextTurn() {
        isPlayerTurn = !isPlayerTurn;
        if ( isPlayerTurn ) {
            // 次ターン開始処理
            turnCount++;
            Vector2Int pos = new Vector2Int((int)player.transform.position.x, (int)player.transform.position.y);
            miniMap.UpdateMap(player.GetPosition(), characterObjects );
        }
        //if ( turnCount % spawnTurnSpan == 0 ) {
        //    SpawnEnemy();
        //}

        if ( false ) {
            Debug.Log( "GameOver" );
        }
    }

    public void LevelUp() {
    }

    public void AttackOnTarget( Vector2 target ) {
        foreach ( var enemy in enemyList ) {
            if ( enemy.transform.position.x == target.x &&
                 enemy.transform.position.y == target.y ) {
                player.AttackOnTarget( enemy );
                if ( false ) {
                    enemyList.Remove( enemy );
                    Destroy( enemy.gameObject );
                }
                break;
            }
        }
    }

    public void NextFloor() {
        currentFloor++;
        DispFloor();

        foreach( var enemy in enemyList ) {
            Destroy( enemy.gameObject );
        }
        enemyList.Clear();
        dungueon.Clear();
        CreateFloor();
    }

    void DispFloor() {
        floorText.text = "F" + currentFloor;
    }

    public void DispPlayerStatus() {
        leftEma.DisplayWeapon(player.GetLeftWeapon());
        rightEma.DisplayWeapon(player.GetRightWeapon());
    }

    public void PlayerTurn( int value ) {
        if ( !isPlayerTurn ) {
            return;
        }

        bool actProceed = false;
        switch( value ) {
            case 0:
                actProceed = player.Move( playerLayer2D, characterObjects, 0 );
                break;
            case 1:
                actProceed = player.Move( playerLayer2D, characterObjects, 1 );
                break;
            case 2:
                actProceed = player.Move( playerLayer2D, characterObjects, 2 );
                break;
            case 3:
                actProceed = player.Move( playerLayer2D, characterObjects, 3 );
                break;
            case 4:
                player.Attack();
                actProceed = true;
                break;
        }

        if ( !actProceed ) {
            return;
        }

        // 移動後のイベント
        ExecuteEvent();
    }

    void ExecuteEvent() {
        Vector2 pos = player.GetPosition();
        int map = playerLayer2D.Get( ( int )pos.x, ( int )pos.y );
        switch ( map ) {
            case ( int )Layer2D.MapValue.Stair:
                NextFloor();
                break;
            default:
                NextTurn();
                break;
        }
    }
}
