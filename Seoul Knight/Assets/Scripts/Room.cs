using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Room : MonoBehaviour
{
    public string type;
    public Vector2 coordinates;

    //  Door Tiles
    public Tile wallRight;
    public Tile wallTopRight;
    public Tile wallLeft;
    public Tile wallTopLeft;
    public Tile wallSideFrontLeft;
    public Tile wallSideFrontRight;

    // Obstacle
    public Tilemap rbTileMap;
    public Tilemap coverTileMap;

    public GameObject column;

    // Start is called before the first frame update
    void Start()
    {
        RoomController.instance.RegisterRoom(this);
        
        List<Vector2> doorPositions = RoomController.instance.doorLocations[coordinates];
        int width = RoomController.instance.width;
        int height = RoomController.instance.height;
        
        foreach (Vector2 doorPosition in doorPositions)
        {
            Vector2 direction = doorPosition - coordinates;

            if (direction == Vector2.up)
            {
                Vector3Int obstaclesVector = new Vector3Int(-2, height / 2 - 1, 0);
                Vector3Int obstaclesSize = new Vector3Int(5, 2, 1);
                TileBase[] tiles = { wallRight, null, null, null, wallLeft, wallTopRight, null, null, null, wallTopLeft };
                rbTileMap.SetTilesBlock(new BoundsInt(obstaclesVector, obstaclesSize), tiles);

            }
            else if (direction == Vector2.down)
            {
                Vector3Int doorPositionVector = new Vector3Int(-1, -height / 2 + 1, 0);
                Vector3Int doorSize = new Vector3Int(3, 1, 1);
                TileBase[] wallTiles = { null, null, null };

                rbTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), wallTiles);

                Vector3Int aboveObstaclesVector = new Vector3Int(-2, -height / 2 + 1, 0);
                Vector3Int aboveObstaclesSize = new Vector3Int(5, 2, 1);
                TileBase[] wallTopTiles = { wallRight, null, null, null, wallLeft, wallTopRight, null, null, null, wallTopLeft };

                coverTileMap.SetTilesBlock(new BoundsInt(aboveObstaclesVector, aboveObstaclesSize), wallTopTiles);

            }
            else if (direction == Vector2.right)
            {
                Vector3Int doorPositionVector = new Vector3Int((width - 1) / 2, -1, 0);
                Vector3Int doorSize = new Vector3Int(1, 4, 1);
                TileBase[] tiles = { null, null, null, wallSideFrontRight };

                rbTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), tiles);
                coverTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), tiles);

            }
            else if (direction == Vector2.left)
            {
                Vector3Int doorPositionVector = new Vector3Int(-(width - 1) / 2, -1, 0);
                Vector3Int doorSize = new Vector3Int(1, 4, 1);
                TileBase[] tiles = { null, null, null, wallSideFrontLeft };

                rbTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), tiles);
                coverTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), tiles);

            }
            
        }

    }

    public void SpawnObjects()
    {
        float screenX, screenY;
        Vector2 pos;

        for (int i = 0; i < noOfObstacles; i++)
        {
            int randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }
}