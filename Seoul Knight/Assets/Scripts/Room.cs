using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public string type;
    public int x;
    public int y;

    public Tile wallHorizontal;
    public Tile wallHorizontalTop;
    public Tile wallLeft;
    public Tile wallRight;

    public Tilemap obstaclesTilemap;

    // Start is called before the first frame update
    void Start()
    {
        RoomController.instance.RegisterRoom(this);

        obstaclesTilemap.SetTile(new Vector3Int(0, 0, 0), wallHorizontal);
       
        TileBase[] nullArray = {null, null, null, null, null, null};
        obstaclesTilemap.SetTilesBlock(new BoundsInt(new Vector3Int(0, 5, 0), new Vector3Int(3, 2, 1)), nullArray);



    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(x * 11, y * 14);
    }
}