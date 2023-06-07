using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public Player player;
    public GameObject reloadBar;
    public Camera cam;
    public WeaponSwitching weaponHolder;

    private Animator animator;

    public int Damage { get; private set; }
    public int KnockbackMultipler { get; private set; }
    public int SuperDamage { get; private set; }
    public int SuperKnockbackMultipler { get; private set; }
    private const float minimumReloadTime = 5/12f;
    private float _reloadTime;
    public float ReloadTime
    {
        get => _reloadTime;
        private set
        {
            if (value < minimumReloadTime)
            {
                _reloadTime = minimumReloadTime;
            }
        }
    }
    public bool IsSuper { get; set; }

    private Vector3 mousePosition;
    private float timeReloaded;



    private void Start()
    {
        animator = GetComponent<Animator>();

        reloadBar.SetActive(false);
        reloadBar.GetComponent<ReloadBar>().SetMaxTime(ReloadTime);
        reloadBar.GetComponent<ReloadBar>().SetTime(ReloadTime);

        Damage = 1;
        KnockbackMultipler = 5;
        SuperDamage = 2;
        SuperKnockbackMultipler = 8;
        IsSuper = false;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Super());
        }

        // Change player facing direction only when no animation is playing
        if (animator.GetCurrentAnimatorClipInfo(0).Length == 0)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookDirection = (mousePosition - transform.position).normalized;

            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            if (lookDirection.x >= 0)
            {
                player.facingRight = true;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                player.facingRight = false;
                transform.eulerAngles = new Vector3(0, 0, 180 + angle);
            }

            //  Sword swing when mouse clicked, no animation is played, and when reloading --> false
            //  *reloadTime will not interrupt the sword swing
            if (Input.GetMouseButtonDown(0) && !weaponHolder.reloading)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
        }

        // Keep reload bar direction constant & Changing delta time & Changing the reload bar
        if (weaponHolder.reloading)
        {
            if (player.facingRight)
            {
                reloadBar.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                reloadBar.transform.localScale = new Vector3(-1, 1, 1);
            }

            timeReloaded += Time.deltaTime;
            reloadBar.GetComponent<ReloadBar>().SetTime(timeReloaded);
        }
    }



    public void SetReloadTime(float newReloadTime)
    {
        ReloadTime = newReloadTime;
        reloadBar.GetComponent<ReloadBar>().SetMaxTime(ReloadTime);
    }


    private IEnumerator Super()
    {
        // Check for glitching out of the map
        // Teleport through wall and into the neighbouring room glitch
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - player.rb.position).normalized * 5;

        const float time = 0.3f;
        float t = time;

        // Check if the player would get stuck in enemy when return to normal
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        IsSuper = true;

        while (t > 0)
        {
            t -= Time.fixedDeltaTime;
            player.rb.MovePosition(player.rb.position + direction * Time.fixedDeltaTime / time);
            yield return new WaitForFixedUpdate();
        }

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        IsSuper = false;
    }



    public IEnumerator Attack()
    {
        timeReloaded = 0;
        weaponHolder.reloading = true;
        reloadBar.SetActive(true);

        yield return new WaitForSeconds(ReloadTime);

        reloadBar.SetActive(false);
        weaponHolder.reloading = false;
    }
}