using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = this.transform.parent.GetComponent<Weapon>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!weapon.IsSuper)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(weapon.Damage, weapon.KnockbackMultipler, transform, true);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(weapon.SuperDamage, weapon.SuperKnockbackMultipler, transform, false);
            }
            
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!weapon.IsSuper)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(weapon.Damage, weapon.KnockbackMultipler, transform, true);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(weapon.SuperDamage, weapon.SuperKnockbackMultipler, transform, false);
            }
        }
    }
}
