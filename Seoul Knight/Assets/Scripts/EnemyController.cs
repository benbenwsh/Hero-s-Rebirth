﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    public Rigidbody2D rb;
    public Material material;
    public SpriteRenderer sprite;
    //public Animator animator;
    public int hp = 2;


    private Color materialTintColour;
    public EnemyAI enemyAI;
    public bool isDead = false;

    public string enemyDirection = "Right";

    private void Start()
    {
        this.materialTintColour = new Color(255, 255, 255, 0);
        this.material.SetColor("_Tint", materialTintColour);
    }



    public void TakeDamage(int damage, Vector2 knockback)
    {
        hp -= damage;

        if (hp > 0)
        {
            StartCoroutine(takeDamageAnimation());
            knockback *= 5;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        else
        {
            //  Die
            isDead = true;
            enemyAI.CancelInvoke();
            knockback *= 20;
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (enemyDirection == "Right")
            {
                transform.rotation = Quaternion.Euler(Vector3.forward * 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.back * 90);
            }
            

            sprite.color = new Color(0.25f, 0.25f, 0.25f, 1);
            gameObject.layer = LayerMask.NameToLayer("Dead Enemy");
        }
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
