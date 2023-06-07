using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Enemy
{

    protected override void Start()
    {
        base.Start();
        Hp = 2;
        Damage = 1;
        Speed = 2;
    }

    public override void Move(Vector2 force)
    {
        rb.MovePosition(rb.position + force);
    }

    protected override void Attack(Player player)
    {
        player.TakeDamage(Damage, this.gameObject);
    }
}
