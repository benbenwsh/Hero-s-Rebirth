using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomEnemyInfo
{

    public int[] GuaranteedEnemies { get; private set; }
    public int[] TotalPointRange { get; private set; }



    public CombatRoomEnemyInfo(int[] guaranteedEnemies, int[] totalPointRange)
    {
        this.GuaranteedEnemies = guaranteedEnemies;
        this.TotalPointRange = totalPointRange;
    }
}
