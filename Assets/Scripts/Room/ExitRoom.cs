using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : Room
{
    public GameObject stairsPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GameObject stairs = Instantiate(stairsPrefab, this.transform, false) as GameObject;
        stairs.transform.localPosition = new Vector3(0.5f, 0.5f, 0);
    }
}
