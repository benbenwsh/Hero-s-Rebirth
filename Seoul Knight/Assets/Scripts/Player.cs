using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    public LayerMask enemyLayers;

    private RoomController roomController;



    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            Vector2 collisionCoordinates = collision.ClosestPoint(transform.position);
            Vector2 currentRoomCoordinates = RoomController.instance.currentRoomCoordinates;
            int width = RoomController.instance.width;
            int height = RoomController.instance.height;
            
            Vector3 newPlayerPosition;
            Debug.Log(collisionCoordinates - currentRoomCoordinates * new Vector2(width, height) * 2);
            //  Right door
            if (collisionCoordinates.x - currentRoomCoordinates.x * width * 2 > 3)
            {
                newPlayerPosition = new Vector3(rb.position.x + width + 1, rb.position.y, 0);
                Debug.Log("1" + newPlayerPosition);
                
                RoomController.instance.currentRoomCoordinates.x++;
            }
            //  Left door
            else if (currentRoomCoordinates.x * width * 2 - collisionCoordinates.x > 3)
            {
                newPlayerPosition = new Vector3(rb.position.x - width - 1, rb.position.y, 0);
                Debug.Log("2" + newPlayerPosition);

                RoomController.instance.currentRoomCoordinates.x--;
            }
            //  Top door
            else if (collisionCoordinates.y - currentRoomCoordinates.y * height * 2 > 3)
            {
                newPlayerPosition = new Vector3(rb.position.x, rb.position.y + height + 3, 0);
                Debug.Log("3" + newPlayerPosition);

                RoomController.instance.currentRoomCoordinates.y++;
            }
            //  Bottom door
            else
            {
                newPlayerPosition = new Vector3(rb.position.x, rb.position.y - height - 3, 0);

                Debug.Log("4" + newPlayerPosition);
                RoomController.instance.currentRoomCoordinates.y--;
            }

            transform.position = newPlayerPosition;
        }
    }

}
