using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator weaponAnimator;
    public bool attacking = false;
    public bool facingRight = true;
    private int damage = 1;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        if (facingRight) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }



    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
        if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Weapon_Attack"))
        {
            Vector2 knockback = collision.transform.position - transform.position;
            knockback = knockback.normalized;

            collision.GetComponent<EnemyController>().TakeDamage(damage, knockback);
        }
    }
}