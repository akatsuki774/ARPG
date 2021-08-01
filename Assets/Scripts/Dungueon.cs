using UnityEngine;
using DTL.Shape;
using MatrixRange = DTL.Base.Coordinate2DimensionalAndLength2Dimensional;
using System.Collections.Generic;

public class Dungueon : MonoBehaviour
{
    private readonly int height = 32;
    private readonly int width = 48;
    private const int outsideWallId = 0;
    private const int insideWallId = 1;
    private const int roomId = 2;
    private const int entranceId = 3;
    private const int wayId = 4;
    private const int stairid = 5;

    public GameObject OutsideWall;
    public GameObject InsideWall;
    public GameObject Room;
    public GameObject Entrance;
    public GameObject Way;
    public GameObject Stair;

    private List<Vector2Int> passablePosList = new List<Vector2Int>();
    private int[,] dungueonMatrix;
    private List<MatrixRange> roomList;

    RogueLike rogueLike = new RogueLike( 0, 1, 2, 3, 4, 30,
        new MatrixRange( 7, 7, 2, 2 ), new MatrixRange( 3, 3, 5, 5 ) );


    public void Create() {
        dungueonMatrix = new int[height, width];
        rogueLike.Draw( dungueonMatrix );
        roomList = rogueLike.GetRoomList();
        SetStair();
        DungeonInstantiate( dungueonMatrix, height, width );
    }

    public void Clear() {
        passablePosList.Clear();
        roomList.Clear();
        foreach( Transform t in this.transform ) {
            Destroy( t.gameObject );
        }
    }

    public bool GetRandomPositionInRoom( ref Vector2Int position ) {
        if ( roomList.Count <= 0 ) {
            return false;
        }

        var room = roomList[Random.Range( 0, roomList.Count - 1 )];
        while( true ) {
            position.x = Random.Range( room.x, room.x + room.w );
            position.y = Random.Range( room.y, room.y + room.h );

            // エントランスが隣になる位置に階段を生成しないようにする
            bool retryFlg = false;
            for ( int i = 0; i < 4; i++ ) {
                Vector2Int vec2 = new Vector2Int( position.x, position.y );
                switch( i ) {
                    case 0:
                        vec2.x += 1;    
                        break;
                    case 1:
                        vec2.y += 1;
                        break;
                    case 2:
                        vec2.x -= 1;
                        break;
                    case 3:
                        vec2.y -= 1;
                        break;
                }
                if ( dungueonMatrix[vec2.y, vec2.x] == entranceId ) {
                    retryFlg = true;
                    break;
                }
            }
            if ( !retryFlg ) {
                break;
            }
        }

        return true;
    }

    public bool GetRandomPosition( ref Vector2Int position, MatrixRange ignoreRange = null ) {
        List<Vector2Int> posList = new List<Vector2Int>( passablePosList );

        if ( !( ignoreRange is null ) ) {
            for ( int x = ignoreRange.x; x < ignoreRange.x + ignoreRange.w; x++ ) {
                for ( int y = ignoreRange.y; y < ignoreRange.y + ignoreRange.h; y++ ) {
                    Vector2Int pos = new Vector2Int( x, y );
                    posList.Remove( pos );
                }
            }
        }
        if ( posList.Count <= 0 ) {
            return false;
        }
        while ( true ) {
            position = posList[Random.Range( 0, posList.Count - 1 )];
            if ( dungueonMatrix[position.y, position.x] == stairid ) {
                continue;
            }
            return true;
        }
    }

    void SetStair() {
        Vector2Int pos = new Vector2Int();
        GetRandomPositionInRoom( ref pos );
        dungueonMatrix[(    int )pos.y, ( int )pos.x] = stairid;
    }

    void DungeonInstantiate( int[,] matrix, int h, int w ) {
        for ( int i = 0; i < h; ++i ) {
            for ( int j = 0; j < w; ++j ) {
                switch ( matrix[i, j] ) {
                    case outsideWallId:
                        Instantiate( OutsideWall, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        break;
                    case insideWallId:
                        Instantiate( InsideWall, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        break;
                    case roomId:
                        Instantiate( Room, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case entranceId:
                        Instantiate( Entrance, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case wayId:
                        Instantiate( Way, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case stairid:
                        Instantiate( Stair, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                }
            }
        }
    }

    public bool CheckPassable( Vector2 pos ) {
        if( pos.x < 0 || pos.x > width ) {
            return false;
        }
        if ( pos.y < 0 || pos.y > height ) {
            return false;
        }
        switch( dungueonMatrix[( int )pos.y, ( int )pos.x] ) {
            case outsideWallId:
            case insideWallId:
                return false;
            case roomId:
            case entranceId:
            case wayId:
            case stairid:
            default:
                return true;
        }
    }

    public Layer2D ConvertToLayer2D() {
        Layer2D layer2d = new Layer2D();
        layer2d.Create( width, height );
        for ( int i = 0; i < height; ++i ) {
            for ( int j = 0; j < width; ++j ) {
                int pos1 = j;
                switch ( dungueonMatrix[i, j] ) {
                    case outsideWallId:
                    case insideWallId:
                        layer2d.Set( j, i, ( int )Layer2D.MapValue.Forbid );
                        break;
                    case roomId:
                    case entranceId:
                    case wayId:
                        layer2d.Set( j, i, ( int )Layer2D.MapValue.Pass );
                        break;
                    case stairid:
                        layer2d.Set( j, i, ( int )Layer2D.MapValue.Stair );
                        break;
                }
            }
        }
        return layer2d;
    }
}
