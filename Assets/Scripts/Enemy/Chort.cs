using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chort : Enemy
{
    
    private const float idleTime = 2f;
    private const float readyTime = 0.5f;
    private const float attackTime = 1f;

    private float timeRemaining;

    private bool idle = false;
    private bool ready = true;
    private bool attack = false;

    protected override void Start()
    {
        base.Start();
        Hp = 10;
        Damage = 2;
        Speed = 5;

        timeRemaining = readyTime;
    }

    public override void Move(Vector2 force)
    {
        if (idle)
        {
            animator.SetBool("idle", true);
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                animator.SetBool("idle", false);
                idle = false;
                ready = true;
                timeRemaining = readyTime;
            }
        }
        else if (ready)
        {
            animator.SetBool("ready", true);
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                animator.SetBool("ready", false);
                ready = false;
                attack = true;
                timeRemaining = attackTime;
            }
        }
        else if (attack)
        {
            animator.SetBool("attack", true);
            timeRemaining -= Time.deltaTime;
            rb.MovePosition(rb.position + force);
            if (timeRemaining <= 0)
            {
                animator.SetBool("attack", false);
                attack = false;
                idle = true;
                timeRemaining = idleTime;
            }
        }
    }

    protected override void Attack(Player player)
    {
        if (attack)
        {
            player.TakeDamage(Damage, this.gameObject);
        }
    }
}
