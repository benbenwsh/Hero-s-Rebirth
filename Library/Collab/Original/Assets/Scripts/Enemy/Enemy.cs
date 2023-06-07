using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public Material material;
    public Animator animator;
    public EnemyAI enemyAI;

    private BoxCollider2D weaponBoxCollider;
    private CombatRoom currentRoom;

    private int hp = 2;
    private bool invincible = false;
    protected int damage
    {
        get
        {
            return 1;
        }
    }

    public bool isDead = false;
    public string enemyDirection = "Right";

    private Color materialTintColour;
    
    

    private void Start()
    {
        this.sprite.material.mainTexture = material.mainTexture;
        this.materialTintColour = new Color(255, 255, 255, 0);
        this.sprite.material.SetColor("_Tint", materialTintColour);

        currentRoom = transform.parent.GetComponent<CombatRoom>();
    }

    private void Update()
    {
        if (invincible)
        {
            if (weaponBoxCollider.isActiveAndEnabled == false)
            {
                Debug.Log("Disabled");
                invincible = false;
            }
        }
    }



    // COLLISION DETECTION
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }

    public void Attack(Player player)
    {
        player.TakeDamage(damage, this.gameObject);
    }

    public void TakeDamage(int damage, Vector2 knockback, int knockbackMultiplier, GameObject player)
    {
        if (!invincible)
        {
            hp -= damage;

            if (hp > 0)
            {
                StartCoroutine(TakeDamageAnimation());
                this.weaponBoxCollider = player.GetComponentInChildren<BoxCollider2D>();
                invincible = true;
                knockback *= knockbackMultiplier;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
            else
            {
                Die(knockback, player.GetComponent<Player>());
            }
        }
    }



    private void Die(Vector2 knockback, Player player)
    {
        isDead = true;
        enemyAI.CancelInvoke();
        animator.enabled = false;

        currentRoom.RemoveEnemy();

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

        // Lifesteal mechanics
        LifeSteal(player);

    }

    private void LifeSteal(Player player)
    {

    }



    IEnumerator TakeDamageAnimation()
    {
        materialTintColour = new Color(255, 255, 255, 255);
        this.sprite.material.SetColor("_Tint", materialTintColour);
        
        yield return new WaitForSeconds(0.1f);

        materialTintColour = new Color(255, 255, 255, 0);
        this.sprite.material.SetColor("_Tint", materialTintColour);
    }



}
