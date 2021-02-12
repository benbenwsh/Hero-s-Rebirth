﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Animator weaponAnimator;
    public Material material;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer weaponSpriteRenderer;
    public GameObject aim;
    public Sprite deathSprite;
    public GameObject gameOverPanel;

    public bool attacking = false;
    public bool facingRight = true;
    public bool playerIsDead = false;
    public HealthBar healthBar;

    private float moveSpeed = 5f;
    private Vector2 movement;
    private int damage = 1;
    private int hp = 3;
    private bool invincible = false;
    private Color flashColour = new Color(255, 255, 255, 0);
    private Color normalColour = new Color(255, 255, 255, 255);
    


    private void Start()
    {
        Time.timeScale = 1f;
        this.material.SetColor("_Tint", flashColour);
        healthBar.SetMaxHealth(hp);
    }



    private void Update()
    {
        playerSpriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        weaponSpriteRenderer.sortingOrder = (int)(-transform.position.y * 100 - 25);

        if (!playerIsDead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            if (movement.sqrMagnitude == 0)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }

            if (facingRight)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }



    private void FixedUpdate()
    {
        if (!playerIsDead)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerIsDead)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                AttackEnemy(collision);

            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Stairs"))
            {
                GameOverMenu.instance.PlayAgain();
            }
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!playerIsDead)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                AttackEnemy(collision);
            }
        }
    }



    private void AttackEnemy(Collider2D collision)
    {
        if (weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName("Weapon_Attack"))
        {
            Vector2 knockback = collision.transform.position - transform.position;
            knockback = knockback.normalized;

            collision.GetComponent<Enemy>().TakeDamage(damage, knockback);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerIsDead)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                TakeDamage(1, collision);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Door"))
            {
                Teleport(collision);
            }
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!playerIsDead)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                
                TakeDamage(1, collision);
            }
        }
    }



    private void Teleport(Collision2D collision)
    {
        Vector2 collisionCoordinates = collision.collider.ClosestPoint(transform.position);
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



    private void TakeDamage(int enemyDamage, Collision2D collision)
    {
        if (!invincible)
        {
            hp -= enemyDamage;
            healthBar.setHealth(hp);

            if (hp > 0) {
                StartCoroutine(TakeDamageAnimation());
            }
            else {
                Die(collision);
            }
        }
    }



    IEnumerator TakeDamageAnimation()
    {
        invincible = true;

        animator.SetTrigger("Hit");

        material.SetColor("_Tint", normalColour);
        yield return new WaitForSeconds(0.05f);
        material.SetColor("_Tint", flashColour);

        for (int i = 0; i < 5; i++)
        {
            playerSpriteRenderer.color = flashColour;
            yield return new WaitForSeconds(0.1f);

            playerSpriteRenderer.color = normalColour;
            yield return new WaitForSeconds(0.1f);
        }

        invincible = false;
    }



    private void Die(Collision2D collision)
    {
        playerIsDead = true;

        if (facingRight)
        {
            animator.SetBool("FacingRight", true);

        }
        else
        {
            animator.SetBool("FacingRight", false);
        }

        animator.SetTrigger("Die");

        aim.SetActive(false);

        playerSpriteRenderer.color = new Color(0.25f, 0.25f, 0.25f, 1);

        Vector2 knockback = transform.position - collision.transform.position;
        knockback = knockback.normalized * 10;
        rb.AddForce(knockback, ForceMode2D.Impulse);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}