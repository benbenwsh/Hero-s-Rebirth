using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Material material;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer weaponSpriteRenderer;
    public GameObject aim;
    public Sprite deathSprite;
    public BoxCollider2D boxCollider;
    [SerializeField] private Weapon weapon;

    public bool attacking = false;
    public bool facingRight = true;
    public bool playerIsDead = false;

    // Properties
    
    public float OriginalSpeed { get; } = 5f;
    public float Speed { get; set; }
    public int MaxHp { get; private set; } = 100;
    private int _hp;
    public int Hp
    {
        get => _hp;
        private set
        {
            if (value < 0)
            {
                _hp = 0;
            }
            else if (value > MaxHp)
            {
                _hp = MaxHp;
            }
            else
            {
                _hp = value;
            }
        }
    }
    private float _lifeStealChance;
    public float LifeStealChance
    {
        get => _lifeStealChance;
        set
        {
            if (value > 0.5f)
            {
                _lifeStealChance = 0.5f;
            }
            else
            {
                _lifeStealChance = value;
            }
        }
    }


    public HealthBar healthBar;

    private Vector2 movement;
    
    private bool invincible = false;
    private Color flashColour = new Color(255, 255, 255, 0);
    private Color normalColour = new Color(255, 255, 255, 255);
    private bool firstRoom = true;



    private void Start()
    {
        LoadGameData();
        SetHp(Hp, MaxHp);

        Speed = OriginalSpeed;

        Time.timeScale = 1f;
        this.material.SetColor("_Tint", flashColour);
        
        

        //AudioManager.instance.Stop("BattleMusic");
        //AudioManager.instance.Stop("PlayerDeath");
        //AudioManager.instance.Play("FirstRoomMusic");
    }



    private void Update()
    {
        playerSpriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        weaponSpriteRenderer.sortingOrder = (int)(-transform.position.y * 100 - 25);

        if (!playerIsDead)
        {
            // Movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Moving animation
            if (movement.sqrMagnitude == 0)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }

            // Changing facing direction
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
        // Movement
        if (!playerIsDead & movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerIsDead)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Door"))
            {
                Teleport(collision);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Collectables"))
            {
                collision.gameObject.GetComponent<Collectables>().Consume(this);
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

        //if (firstRoom)
        //{
        //    AudioManager.instance.Stop("FirstRoomMusic");
        //    AudioManager.instance.Play("BattleMusic");
        //    firstRoom = false;
        //}

        //  Right door
        if (collisionCoordinates.x - currentRoomCoordinates.x * width * 2 > 3)
        {
            newPlayerPosition = new Vector3(rb.position.x + width + 1, rb.position.y, 0);

            RoomController.instance.currentRoomCoordinates.x++;
        }
        //  Left door
        else if (currentRoomCoordinates.x * width * 2 - collisionCoordinates.x > 3)
        {
            newPlayerPosition = new Vector3(rb.position.x - width - 1, rb.position.y, 0);

            RoomController.instance.currentRoomCoordinates.x--;
        }
        //  Top door
        else if (collisionCoordinates.y - currentRoomCoordinates.y * height * 2 > 3)
        {
            newPlayerPosition = new Vector3(rb.position.x, rb.position.y + height + 3, 0);

            RoomController.instance.currentRoomCoordinates.y++;
        }
        //  Bottom door
        else
        {
            newPlayerPosition = new Vector3(rb.position.x, rb.position.y - height - 3, 0);

            RoomController.instance.currentRoomCoordinates.y--;
        }

        transform.position = newPlayerPosition;
    }



    public void SetHp(int newHP, int newMaxHP)
    {
        Hp = newHP;
        MaxHp = newMaxHP;
        healthBar.SetHP(newHP, newMaxHP);
    }


    public void TakeDamage(int enemyDamage, GameObject collisionGameObject)
    {
        if (!invincible && !playerIsDead)
        {
            SetHp(Hp - enemyDamage, MaxHp);

            if (Hp > 0) {
                StartCoroutine(TakeDamageAnimation());
            }
            else {
                Die(collisionGameObject);
            }
        }
    }



    private IEnumerator TakeDamageAnimation()
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

    //IEnumerator PlayDead()
    //{
    //    AudioManager.instance.Stop("BattleMusic");
    //    yield return new WaitForSeconds(0.1f);
    //    AudioManager.instance.Play("PlayerDeath");
    //}

    private void Die(GameObject collisionGameObject)
    {
        playerIsDead = true;
        animator.enabled = false;
        aim.SetActive(false);
        //StartCoroutine(PlayDead());

        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.back * 90);
        }

        playerSpriteRenderer.color = new Color(0.25f, 0.25f, 0.25f, 1);

        Vector2 knockback = boxCollider.bounds.center - collisionGameObject.GetComponent<BoxCollider2D>().bounds.center;
        knockback = knockback.normalized * 10;
        rb.AddForce(knockback, ForceMode2D.Impulse);
        StartCoroutine(GameOverMenu.instance.GameOver());
    }



    private void LoadGameData()
    {
        GameData data = SaveSystem.LoadGameData();

        if (data == null)
        {
            Hp = MaxHp;
        }
        else
        {
            Hp = data.hp;
            MaxHp = data.maxHp;
        }
    }
}