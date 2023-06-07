using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Room : MonoBehaviour
{
    public Vector2 roomCoordinates;

    //  Door Tiles
    public Tile wallRight;
    public Tile wallTopRight;
    public Tile wallLeft;
    public Tile wallTopLeft;
    public Tile wallSideFrontLeft;
    public Tile wallSideFrontRight;

    //  Obstacle
    public Tilemap rbTileMap;
    public Tilemap coverTileMap;

    protected int width;
    protected int height;

    protected List<Vector2> doorCoordinates;

    private List<Vector2> objectCoordinates;



    protected virtual void Start()
    {
        width = RoomController.instance.width;
        height = RoomController.instance.height;

        doorCoordinates = RoomController.instance.doorLocations[roomCoordinates];

        objectCoordinates = new List<Vector2>();

        foreach (Vector2 door in doorCoordinates)
        {
            Vector2 direction = door - roomCoordinates;

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
                TileBase[] wallTiles = { null, null, null, wallSideFrontRight };
                TileBase[] wallTopTiles = { null, null, null, null };

                rbTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), wallTiles);
                coverTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), wallTopTiles);

            }
            else if (direction == Vector2.left)
            {
                Vector3Int doorPositionVector = new Vector3Int(-(width - 1) / 2, -1, 0);
                Vector3Int doorSize = new Vector3Int(1, 4, 1);
                TileBase[] wallTiles = { null, null, null, wallSideFrontLeft };
                TileBase[] wallTopTiles = { null, null, null, null };

                rbTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), wallTiles);
                coverTileMap.SetTilesBlock(new BoundsInt(doorPositionVector, doorSize), wallTopTiles);

            }
        }
    }





    public void RandomObjectsSpawner(int noOfObjects, GameObject prefab)
    {
        Debug.Log(prefab);
        while (noOfObjects > 0)
        {
            int x = Random.Range(-(width - 3) / 2, (width - 3) / 2);
            int y = Random.Range(-height / 2 + 3, height / 2 - 1);
            Vector2 coordinates = new Vector2(x + 0.5f, y + 0.5f);

            if (!objectCoordinates.Contains(coordinates))
            {
                Debug.Log("Spawned");
                GameObject gameObject = Instantiate(prefab, this.transform, false) as GameObject;

                gameObject.transform.localPosition = coordinates;

                objectCoordinates.Add(coordinates);
                noOfObjects--;
            }
        }
    }

}