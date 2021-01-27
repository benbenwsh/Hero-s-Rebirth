using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Room : MonoBehaviour
{
    public string type;
    public int x;
    public int y;
    public int width;
    public int height;

    public Tile wallHorizontal;
    public Tile wallHorizontalTop;
    public Tile wallLeft;
    public Tile wallRight;

    public Tilemap obstaclesTilemap;

    // Start is called before the first frame update
    void Start()
    {
        RoomController.instance.RegisterRoom(this);

        List<(int, int)> doorPositions = RoomController.instance.doorLocations[(x, y)];

        
        foreach ((int, int) doorPosition in doorPositions)
        {

            (int, int) direction = (doorPosition.Item1 - x, doorPosition.Item2 - y);
            TileBase[] nullArray = { null, null, null };

            if (direction == (0, 1))
            {
                Vector3Int doorPositionVector = new Vector3Int(-1, (height - 1) / 2, 0);
                Vector3Int doorSize = new Vector3Int(3, 1, 1);
                obstaclesTilemap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), nullArray);
                Debug.Log((x, y).ToString() + doorPositionVector.ToString());
            }
            else if (direction == (0, -1))
            {
                Vector3Int doorPositionVector = new Vector3Int(-1, -(height - 1) / 2, 0);
                Vector3Int doorSize = new Vector3Int(3, 1, 1);
                obstaclesTilemap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), nullArray);
                Debug.Log((x, y).ToString() + doorPositionVector.ToString());
            }
            else if (direction == (1, 0))
            {
                Vector3Int doorPositionVector = new Vector3Int((width - 1) / 2, -1, 0);
                Vector3Int doorSize = new Vector3Int(1, 3, 1);
                obstaclesTilemap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), nullArray);
                Debug.Log((x, y).ToString() + doorPositionVector.ToString());
            }
            else if (direction == (-1, 0))
            {
                Vector3Int doorPositionVector = new Vector3Int(-(width - 1) / 2, -1, 0);
                Vector3Int doorSize = new Vector3Int(1, 3, 1);
                obstaclesTilemap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), nullArray);
                Debug.Log((x, y).ToString() + doorPositionVector.ToString());
            }



                
            

            
        }

    }
}