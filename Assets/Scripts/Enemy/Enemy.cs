using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy: MonoBehaviour
{
    [SerializeField] protected SpriteRenderer enemySpriteRenderer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Material material;
    [SerializeField] protected Animator animator;
    [SerializeField] protected EnemyAI enemyAI;
    [SerializeField] protected GameObject floatingTextPrefab;
    

    private BoxCollider2D weaponBoxCollider;
    protected CombatRoom currentRoom;
    protected Color materialTintColour;

    protected int Hp { get;  set; }
    protected int Damage { get; set; }
    public int Speed { get; protected set; }

    private bool invincible = false;

    public bool isDead = false;
    public string enemyDirection = "Right";
    
    

    protected virtual void Start()
    {
        this.enemySpriteRenderer.material.mainTexture = material.mainTexture;
        this.materialTintColour = new Color(255, 255, 255, 0);
        this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);

        currentRoom = transform.parent.GetComponent<CombatRoom>();
    }

    protected virtual void Update()
    {
        enemySpriteRenderer.sortingOrder = (int)(-transform.position.y * 100);

        if (invincible)
        {
            if (weaponBoxCollider.isActiveAndEnabled == false)
            {
                invincible = false;
            }
        }
    }




    // COLLISION DETECTION
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Attack(collision.gameObject.GetComponent<Player>());
        }
    }



    public abstract void Move(Vector2 force);
    

    protected abstract void Attack(Player player);



    public void TakeDamage(int damage, int knockbackMultiplier, Transform collisionTransform, bool isWeapon)
    {
        if (!invincible)
        {
            Hp -= damage;
            Vector2 knockback = (transform.position - collisionTransform.position).normalized;
            if (Hp > 0)
            {
                StartCoroutine(TakeDamageAnimation(isWeapon));
                this.weaponBoxCollider = collisionTransform.GetComponent<BoxCollider2D>();
                invincible = true;
                rb.AddForce(knockback * knockbackMultiplier, ForceMode2D.Impulse);
            }
            else
            {
                Die(knockback, collisionTransform.GetComponentInParent<Player>());
            }
        }
    }



    protected virtual void Die(Vector2 knockback, Player player)
    {
        isDead = true;
        enemyAI.CancelInvoke();
        animator.enabled = false;

        currentRoom.RemoveEnemy();

        knockback *= 20;
        rb.AddForce(knockback, ForceMode2D.Impulse);

        // Die animation
        if (enemyDirection == "Right")
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.back * 90);
        }

        enemySpriteRenderer.color = new Color(0.25f, 0.25f, 0.25f, 1);
        materialTintColour = new Color(1, 1, 1, 0);
        this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);
        gameObject.layer = LayerMask.NameToLayer("Dead Enemy");

        LifeSteal(player);
    }



    private void LifeSteal(Player player)
    {
        if (player.Hp < player.MaxHp)
        {
            float randomNo = Random.Range(0f, 1f);
            if (randomNo < player.LifeStealChance)
            {
                GameObject emptyGameObject = new GameObject();
                emptyGameObject.transform.position = player.gameObject.transform.position;
                GameObject floatingText = Instantiate(floatingTextPrefab, emptyGameObject.transform, false);
                floatingText.GetComponent<FloatingText>().SetText("LIFE STEAL +1");
                Destroy(emptyGameObject, 0.75f);

                player.SetHp(player.Hp + 1, player.MaxHp);
            }
        }
    }



    private IEnumerator TakeDamageAnimation(bool isWeapon)
    {
        materialTintColour = new Color(255, 255, 255, 255);
        this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);
        yield return new WaitForSeconds(0.1f);

        materialTintColour = new Color(255, 255, 255, 0);
        this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);

        if (!isWeapon)
        {
            invincible = false;
        }
    }
}
