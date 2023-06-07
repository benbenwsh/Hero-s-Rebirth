using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedChest : Chest
{
    [SerializeField] private GameObject wogolPrefab;

    protected override void OpenChest()
    {
        if (!opened)
        {
            animator.SetTrigger("Open");

            TrappedChestRoom tcr = this.transform.parent.GetComponent<TrappedChestRoom>();
            tcr.RandomObjectsSpawner(tcr.noOfEnemies, wogolPrefab);
            tcr.CloseDoor();

            opened = true;
        }
    }
}
