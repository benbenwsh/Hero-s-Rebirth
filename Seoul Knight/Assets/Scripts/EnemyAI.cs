using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWayPointdistance = 3f;

    Path path;
    private int currentWaypoint = 0;

    public Seeker seeker;
    public Rigidbody2D rb;

    public EnemyController enemyController;

    


    void Start()
    {
        InvokeRepeating("UpdatePath", 0, .5f);
    }



    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        if (!enemyController.isDead)
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



            if (direction.x > 0)
            {
                enemyController.enemyDirection = "Right";
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                enemyController.enemyDirection = "Left";
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }
}