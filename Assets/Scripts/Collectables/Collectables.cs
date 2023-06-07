using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectables : MonoBehaviour
{

    [SerializeField] protected GameObject floatingTextPrefab;



    public abstract void Consume(Player player);

    protected void CreateFloatingText(string text)
    {
        GameObject emptyGameObject = new GameObject();
        emptyGameObject.transform.position = this.transform.position;
        GameObject floatingText = Instantiate(floatingTextPrefab, emptyGameObject.transform, false);
        floatingText.GetComponent<FloatingText>().SetText(text);
        Destroy(emptyGameObject, 0.75f);
    }
}
