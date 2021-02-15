using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private BoxCollider2D targetBoxCollider;

    public float speed = 150f;
    public float nextWayPointdistance = 3f;

    Path path;
    private int currentWaypoint = 0;

    public Seeker seeker;
    public Rigidbody2D rb;
    public Animator animator;

    public Enemy enemy;


    void Start()
    {
        targetBoxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
    }



    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetBoxCollider.bounds.center, OnPathComplete);
        } 
    }


    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }



    private void FixedUpdate()
    {
        if (!enemy.isDead)
        {
            if (path == null || currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }
                
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);



            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWayPointdistance)
            {
                currentWaypoint++;
            }

            if (force.sqrMagnitude == 0)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                if (direction.x >= 0)
                {
                    enemy.enemyDirection = "Right";

                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    enemy.enemyDirection = "Left";

                    transform.localScale = new Vector3(-1, 1, 1);
                }

                animator.SetBool("Moving", true);
            }
        }
        
    }
}