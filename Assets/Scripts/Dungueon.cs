using UnityEngine;
using DTL.Shape;
using MatrixRange = DTL.Base.Coordinate2DimensionalAndLength2Dimensional;
using System.Collections.Generic;

public class Dungueon : MonoBehaviour
{
    public enum ChipID
    {
        OutsideWallId = 0,
        InsideWallId = 1,
        RoomId = 2,
        EntranceId = 3,
        WayId = 4,
        Stairid = 5,
    }

    [SerializeField]
    private GameObject outsideWallPrefab;
    [SerializeField]
    private GameObject insideWallPrefab;
    [SerializeField]
    private GameObject roomPrebab;
    [SerializeField]
    private GameObject entrancePrefab;
    [SerializeField]
    private GameObject wayPrefab;
    [SerializeField]
    private GameObject stairPrefab;

    private List<Vector2Int> passablePosList = new List<Vector2Int>();
    private int[,] dungueonMatrix;
    private List<MatrixRange> roomList;

    private RogueLike rogueLike = new RogueLike( ( int )ChipID.OutsideWallId,
                                                ( int )ChipID.InsideWallId,
                                                ( int )ChipID.RoomId,
                                                ( int )ChipID.EntranceId,
                                                ( int )ChipID.WayId, 30,
        new MatrixRange( 7, 7, 2, 2 ), new MatrixRange( 3, 3, 5, 5 ) );

    public void Create( int width, int height ) {
        dungueonMatrix = new int[height, width];
        rogueLike.Draw( dungueonMatrix );
        roomList = rogueLike.GetRoomList();
        SetStair( ref dungueonMatrix );
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
        var room = roomList[Random.Range( 0, roomList.Count - 1 )];
        position.x = Random.Range( room.x, room.x + room.w );
        position.y = Random.Range( room.y, room.y + room.h );
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
            if ( dungueonMatrix[position.y, position.x] == ( int )ChipID.Stairid ) {
                continue;
            }
            return true;
        }
    }

    public ChipID CheckChipID( Vector2Int pos )
    {
        if ( pos.x < 0 || pos.x > dungueonMatrix.GetLength( 1 ) )
        {
            return ChipID.OutsideWallId;
        }
        if ( pos.y < 0 || pos.y > dungueonMatrix.GetLength( 0 ) )
        {
            return ChipID.OutsideWallId;
        }
        return ( ChipID )dungueonMatrix[pos.y, pos.x];
    }
    public Layer2D ConvertToLayer2D( bool stairIsForbid = false )
    {
        Layer2D layer2d = new Layer2D();
        layer2d.Create( dungueonMatrix.GetLength( 1 ), dungueonMatrix.GetLength( 0 ) );
        for ( int i = 0; i < dungueonMatrix.GetLength( 0 ); ++i )
        {
            for ( int j = 0; j < dungueonMatrix.GetLength( 1 ); ++j )
            {
                int pos1 = j;
                switch ( ( ChipID )dungueonMatrix[i, j] )
                {
                    case ChipID.OutsideWallId:
                    case ChipID.InsideWallId:
                        layer2d.Set( j, i, ( int )Layer2D.MapValue.Forbid );
                        break;
                    case ChipID.RoomId:
                    case ChipID.EntranceId:
                    case ChipID.WayId:
                        layer2d.Set( j, i, ( int )Layer2D.MapValue.Pass );
                        break;
                    case ChipID.Stairid:
                        if ( !stairIsForbid )
                        {
                            layer2d.Set( j, i, ( int )Layer2D.MapValue.Stair );
                        }
                        else
                        {
                            layer2d.Set( j, i, ( int )Layer2D.MapValue.Forbid );
                        }
                        break;
                }
            }
        }
        return layer2d;
    }

    private void SetStair( ref int[,] matrix ) {
        Vector2Int[] direction = { new Vector2Int( -1, 0 ),
                                    new Vector2Int( 1, 0 ),
                                    new Vector2Int( 0, -1 ),
                                    new Vector2Int( 0, 1 ) };
        Vector2Int pos = new Vector2Int();

        while ( true )
        {
            GetRandomPositionInRoom( ref pos );
            var findFlg = true;
            foreach ( var d in direction )
            {
                // エントランスが隣になる位置に階段を生成しないようにする
                if ( matrix[pos.y + d.y, pos.x + d.x] == ( int )ChipID.EntranceId )
                {
                    findFlg = false;
                    break;
                }
            }
            if( findFlg )
            {
                break;
            }
        }
        matrix[pos.y, pos.x] = ( int )ChipID.Stairid;
    }

    private void DungeonInstantiate( int[,] matrix, int h, int w ) {
        for ( int i = 0; i < h; ++i ) {
            for ( int j = 0; j < w; ++j ) {
                switch ( ( ChipID )matrix[i, j] ) {
                    case ChipID.OutsideWallId:
                        Instantiate( outsideWallPrefab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        break;
                    case ChipID.InsideWallId:
                        Instantiate( insideWallPrefab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        break;
                    case ChipID.RoomId:
                        Instantiate( roomPrebab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case ChipID.EntranceId:
                        Instantiate( entrancePrefab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case ChipID.WayId:
                        Instantiate( wayPrefab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                    case ChipID.Stairid:
                        Instantiate( stairPrefab, new Vector3( j, i, 0 ), Quaternion.identity, this.transform );
                        passablePosList.Add( new Vector2Int( j, i ) );
                        break;
                }
            }
        }
    }
}
