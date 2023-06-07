using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalCombatRoom : CombatRoom
{

    [SerializeField] protected GameObject columnPrefab;

    private const int noOfObstacles = 5;

    public int Level { protected get; set; }
    public int EnemyType { protected get; set; }
    public int TotalPoint { protected get; set; }



    protected override void Start()
    {
        base.Start();

        CloseDoor();
        RandomObjectsSpawner(noOfObstacles, columnPrefab);
        EnemySpawner();
        StartCoroutine(PrepareCombatRoom());
    }

    protected abstract void EnemySpawner();
}
