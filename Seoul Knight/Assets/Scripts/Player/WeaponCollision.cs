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
            AttackEnemy(collision);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AttackEnemy(collision);
        }
    }



    private void AttackEnemy(Collider2D collision)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Weapon_Attack"))
        {
            Vector2 knockback = collision.transform.position - transform.position;

            collision.GetComponent<Enemy>().TakeDamage(weapon.damage, knockback.normalized, weapon.knockbackMultipler);
        }
    }
}
