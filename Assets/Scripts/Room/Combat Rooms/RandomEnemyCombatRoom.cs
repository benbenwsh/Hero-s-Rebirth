using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyCombatRoom : NormalCombatRoom
{

    protected override void Start()
    {
        base.Start();
    }


    protected override void EnemySpawner()
    {
        while (TotalPoint > 0)
        {
            int randomNo = Random.Range(0, enemyPrefabs[Level/3].Length);

            // What if it is not possible make TotalPoint = 0?
            int enemyPoint = enemyPoints[Level/3][randomNo];
            if (enemyPoint <= TotalPoint)
            {
                TotalPoint -= enemyPoints[Level/3][randomNo];
                RandomObjectsSpawner(1, enemyPrefabs[Level/3][randomNo]);
                noOfEnemies++;
            }
        }
    }
}
