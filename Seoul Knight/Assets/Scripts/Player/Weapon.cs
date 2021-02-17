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

    public int damage;
    public int knockbackMultipler;
    public float reloadTime;

    private Vector3 mousePosition;
    private float timeReloaded;



    private void Start()
    {
        animator = GetComponent<Animator>();

        reloadBar.SetActive(false);
        reloadBar.GetComponent<ReloadBar>().SetMaxTime(reloadTime);
    }



    private void Update()
    {
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

            //  Attack animation
            if (Input.GetMouseButtonDown(0) && !weaponHolder.reloading)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
        }

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



   



    public IEnumerator Attack()
    {
        timeReloaded = 0;
        weaponHolder.reloading = true;
        reloadBar.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        reloadBar.SetActive(false);
        weaponHolder.reloading = false;
    }
}
