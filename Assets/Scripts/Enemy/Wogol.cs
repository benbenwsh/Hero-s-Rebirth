using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wogol : Enemy
{

    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private GameObject particleSystemPrefab;

    private Player player;
    private float stoppingRadius = 1f;
    private float triggerRadius = 2f;
    private float damageRadius = 3f;
    private bool exploding = false;



    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").GetComponent<Player>();
        Hp = 2;
        Damage = 5;
        Speed = 3;
    }


    protected override void Update()
    {
        base.Update();
        Attack(player);
    }



    protected override void OnCollisionEnter2D(Collision2D collision) { }

    protected override void OnCollisionStay2D(Collision2D collision) { }



    public override void Move(Vector2 force)
    {
        if (Vector2.Distance(boxCollider.bounds.center, player.GetComponent<BoxCollider2D>().bounds.center) > stoppingRadius)
        {
            rb.MovePosition(rb.position + force);
        }
    }



    protected override void Attack(Player player)
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= triggerRadius && !exploding)
        {
            exploding = true;
            StartCoroutine("Explosion");
        }
    }



    private IEnumerator Explosion()
    {
        for (int i = 0; i < 10; i++)
        {
            materialTintColour = new Color(218/255f, 78/255f, 55/255f, 1);
            this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);

            yield return new WaitForSeconds(0.1f);

            materialTintColour = new Color(1, 1, 1, 0);
            this.enemySpriteRenderer.material.SetColor("_Tint", materialTintColour);

            yield return new WaitForSeconds(0.1f);
        }
        
        if (Vector2.Distance(transform.position, player.transform.position) < damageRadius)
        {
            player.TakeDamage(Damage, this.gameObject);
        }

        GameObject emptyGameObject = new GameObject();
        emptyGameObject.transform.position = this.transform.position;
        GameObject particleSystem = Instantiate(particleSystemPrefab, emptyGameObject.transform, false);
        particleSystem.GetComponent<ParticleSystem>().Play();
        Destroy(emptyGameObject, 0.75f);

        currentRoom.RemoveEnemy();

        Destroy(this.gameObject);
    }

    protected override void Die(Vector2 knockback, Player player)
    {
        base.Die(knockback, player);
        StopCoroutine("Explosion");
    }
}
