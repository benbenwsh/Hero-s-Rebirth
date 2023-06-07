using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private BoxCollider2D targetBoxCollider;
    public float nextWayPointdistance = 1f;

    public Path path { get; private set; }
    private int currentWaypoint = 0;

    [SerializeField] private Seeker seeker;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;

    public Enemy enemy;


    void Start()
    {
        targetBoxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        InvokeRepeating("UpdatePath", 0, 1f);
    }



    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            // Calculate path & Call OnPathComplete when finished calculating path
            seeker.StartPath(boxCollider.bounds.center, targetBoxCollider.bounds.center, OnPathComplete);
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
            if (path == null)
            {
                return;
            }


            Vector2 direction;

            if (currentWaypoint >= path.vectorPath.Count - 1)
            {
                direction = targetBoxCollider.bounds.center - boxCollider.bounds.center;
            }
            else
            {
                direction = path.vectorPath[currentWaypoint+1] - boxCollider.bounds.center;
                float distance = Vector2.Distance(boxCollider.bounds.center, path.vectorPath[currentWaypoint]);
                if (distance < nextWayPointdistance)
                {
                    currentWaypoint++;
                }
            }

            Vector2 force = direction.normalized * enemy.Speed * Time.deltaTime;

            enemy.Move(force);


            // Enemy facing direction
            if (force.x > 0)
            {
                enemy.enemyDirection = "Right";

                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (force.x < 0)
            {
                enemy.enemyDirection = "Left";

                transform.localScale = new Vector3(-1, 1, 1);
            }

            
        }
    }
}