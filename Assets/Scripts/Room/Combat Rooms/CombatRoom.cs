using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class CombatRoom : Room
{
    
    [SerializeField] protected GameObject doorPrefab;

    [SerializeField] protected GameObject[] demonPrefabs;
    [SerializeField] protected GameObject[] undeadPrefabs;
    [SerializeField] protected GameObject[] orcPrefabs;
    
    [SerializeField] protected GameObject[][] enemyPrefabs;



    protected override void Start()
    {
        base.Start();
        enemyPrefabs = new GameObject[][] { demonPrefabs, undeadPrefabs, orcPrefabs };
    }



    protected int[][] enemyPoints = new int[3][] {
        new int[] { 1, 2, 3 },
        new int[] { 1, 2, 3 },
        new int[] { 1, 2, 3 }
    };

    public int noOfEnemies;


    
    private void CreateCombatRoomDoor(Vector2 doorLocalPosition)
    {
        GameObject door = Instantiate(doorPrefab, this.transform, false) as GameObject;

        door.transform.localPosition = doorLocalPosition;
        door.tag = "Door";
        door.GetComponent<SpriteRenderer>().sortingOrder = (int)(-door.transform.position.y * 100);
    }



    public void CloseDoor()
    {
        foreach (Vector2 door in doorCoordinates)
        {
            Vector2 direction = door - roomCoordinates;

            if (direction == Vector2.up)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateCombatRoomDoor(new Vector2(i - 0.5f, height / 2 - 0.5f));
                }
            }
            else if (direction == Vector2.down)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateCombatRoomDoor(new Vector2(i - 0.5f, -height / 2 + 1.5f));
                }
            }
            else if (direction == Vector2.right)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateCombatRoomDoor(new Vector2((width - 1) / 2 + 0.5f, i - 0.5f));
                }
            }
            else if (direction == Vector2.left)
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateCombatRoomDoor(new Vector2(-(width - 1) / 2 + 0.5f, i - 0.5f));
                }
            }
        }
    }



    public void RemoveEnemy()
    {
        noOfEnemies--;

        if (noOfEnemies == 0)
        {
            RemoveDoors();
        }
    }


    private void RemoveDoors()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Door")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    protected IEnumerator PrepareCombatRoom()
    {
        AstarData data = AstarPath.active.data;
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
        gg.center = new Vector3(roomCoordinates.x * width * 2 + 0.5f, roomCoordinates.y * height * 2 + 0.5f, 0);
        gg.SetDimensions((width - 1) * 2, (height - 3) * 2, 0.5f);
        gg.rotation = new Vector3(-90, 0, 0);

        GraphCollision graphCollision = new GraphCollision();
        graphCollision.use2D = true;
        graphCollision.mask = LayerMask.GetMask("Obstacle");
        graphCollision.diameter = 1.5f;
        gg.collision = graphCollision;

        yield return null;

        AstarPath.active.Scan(gg);
    }
}
