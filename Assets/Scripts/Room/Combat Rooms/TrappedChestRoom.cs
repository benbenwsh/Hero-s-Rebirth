using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrappedChestRoom : CombatRoom
{
    [SerializeField] private GameObject trappedChestPrefab;

    protected override void Start()
    {
        base.Start();

        GameObject chest = Instantiate(trappedChestPrefab, this.transform, false) as GameObject;
        chest.transform.localPosition = new Vector3(0.5f, 0.5f, 0);
        chest.GetComponent<SpriteRenderer>().sortingOrder = (int)(-gameObject.transform.position.y * 100 - 100);

        noOfEnemies = 3;

        StartCoroutine(PrepareCombatRoom());
    }
}