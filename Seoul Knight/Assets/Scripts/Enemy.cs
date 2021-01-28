using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{

    public Rigidbody2D rb;
     
    public Material material;
    private Color materialTintColour;

    private void Start()
    {
        
        this.materialTintColour = new Color(255, 255, 255, 0);
        this.material.SetColor("_Tint", materialTintColour);
    }



    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(takeDamageAnimation());
        Vector2 knockback = transform.position - collision.transform.position;
        knockback = knockback.normalized;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    IEnumerator takeDamageAnimation()
    {
        materialTintColour = new Color(255, 255, 255, 255);
        material.SetColor("_Tint", materialTintColour);

        
        yield return new WaitForSeconds(0.05f);

        materialTintColour = new Color(255, 255, 255, 0);
        material.SetColor("_Tint", materialTintColour);
    }


}
