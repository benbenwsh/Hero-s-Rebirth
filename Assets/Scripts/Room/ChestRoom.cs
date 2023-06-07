using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRoom : Room
{

    [SerializeField] private GameObject chestPrefab;

    protected override void Start()
    {
        base.Start();

        GameObject chest = Instantiate(chestPrefab, this.transform, false) as GameObject;
        chest.transform.localPosition = new Vector3(0.5f, 0.5f, 0);
        chest.GetComponent<SpriteRenderer>().sortingOrder = (int)(-gameObject.transform.position.y * 100 - 100);
    }
}
