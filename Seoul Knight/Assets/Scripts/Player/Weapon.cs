using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    private Player player;
    
    private SpriteRenderer weaponSpriteRenderer;

    private Animator animator;
    private GameObject reloadBar;
    private WeaponSwitching weaponHolder;

    public int damage;
    public int knockbackMultipler;
    public float reloadTime;

    private Vector3 mousePosition;
    private float timeReloaded;

    private void Awake()
    {
        reloadBar = GameObject.Find("Reload Bar");
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        
        weaponHolder = transform.parent.gameObject.GetComponent<WeaponSwitching>();
        weaponSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        //reloadBar.SetActive(false);
    }



    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length == 0)
        {
            player.ChangeFacingDirection(transform);

            //  Attack animation
            if (Input.GetMouseButtonDown(0) && !weaponHolder.reloading)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
        }

        if (weaponHolder.reloading)
        {
            timeReloaded += Time.deltaTime;
            reloadBar.GetComponent<ReloadBar>().SetTime(timeReloaded);
        }

        weaponSpriteRenderer.sortingOrder = (int)(-transform.position.y * 100 - 25);
    }



   



    public IEnumerator Attack()
    {
        timeReloaded = 0;
        reloadBar.GetComponent<ReloadBar>().SetMaxTime(reloadTime);
        reloadBar.SetActive(true);
        weaponHolder.reloading = true;

        yield return new WaitForSeconds(reloadTime);

        weaponHolder.reloading = false;
        reloadBar.SetActive(false);
    }
}
