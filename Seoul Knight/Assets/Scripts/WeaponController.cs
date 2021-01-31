using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    public Transform aimTransform;
    public Camera cam;
    public Animator animator;

    private Vector3 mousePosition;
    private float reloadTime = 0.5f;
    private bool reloading = false;
    private string[] animationNames = { "Weapon_Attack_Before", "Weapon_Attack", "Weapon_Attack_After" };

    public Player player;



    // Update is called once per frame
    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0).Length == 0)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookDirection = (mousePosition - aimTransform.position).normalized;

            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            if (lookDirection.x >= 0)
            {
                player.facingRight = true;
                aimTransform.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                player.facingRight = false;
                aimTransform.eulerAngles = new Vector3(0, 0, 180 + angle);
            }


            //  Attack animation
            if (Input.GetMouseButtonDown(0) && !reloading)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(Attack());
            }
        }

        


        

    }

    public IEnumerator Attack()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
