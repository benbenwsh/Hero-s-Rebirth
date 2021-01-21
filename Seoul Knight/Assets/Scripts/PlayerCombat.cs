using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform meleeAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Attack", true);
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Attack", false);

        }
    }

    void Attack()
    {
        Debug.Log("hello");
    }

}
