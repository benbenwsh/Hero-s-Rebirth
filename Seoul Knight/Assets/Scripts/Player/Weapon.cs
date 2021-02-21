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
    public SpriteRenderer weaponSpriteRenderer;

    private Animator animator;

    public int damage;
    public int knockbackMultipler;
    public float reloadTime;

    private Vector3 mousePosition;
    private float timeReloaded;



    private void Start()
    {
        animator = GetComponent<Animator>();

        reloadBar.SetActive(false);
    }



    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length == 0)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            player.ChangeFacingDirection(transform, angle, lookDirection.x >= 0);

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
