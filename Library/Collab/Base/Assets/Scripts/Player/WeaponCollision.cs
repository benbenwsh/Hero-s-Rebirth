using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private Weapon weapon;
    private Animator animator;

    private void Start()
    {
        weapon = this.transform.parent.GetComponent<Weapon>();
        animator = this.transform.parent.GetComponent<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AttackEnemy(collision, this.gameObject.GetComponentInParent<Player>());
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AttackEnemy(collision, this.gameObject.GetComponentInParent<Player>());
        }
    }



    private void AttackEnemy(Collider2D collision, Player player)
    {
        Vector2 knockback = collision.transform.position - transform.position;

        collision.gameObject.GetComponent<Enemy>().TakeDamage(weapon.damage, knockback.normalized, weapon.knockbackMultipler, this.gameObject.GetComponent<BoxCollider2D>());
    }
}
