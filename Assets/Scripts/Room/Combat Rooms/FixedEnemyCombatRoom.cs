using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemyCombatRoom : NormalCombatRoom
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void EnemySpawner()
    {
        noOfEnemies = TotalPoint / enemyPoints[Level / 3][EnemyType];
        RandomObjectsSpawner(noOfEnemies, enemyPrefabs[Level/3][EnemyType]);
    }
}
