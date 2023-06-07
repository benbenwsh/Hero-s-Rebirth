using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 100);
    }
}
