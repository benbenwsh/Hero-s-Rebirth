using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public bool reloading = false;

    private int selectedWeaponIndex = 0;



    private void Start()
    {
        SelectWeapon();
    }



    private void Update()
    {
        if (!reloading)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                selectedWeaponIndex = (selectedWeaponIndex + 1) % 2;
                SelectWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeaponIndex = 0;
                SelectWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedWeaponIndex = 1;
                SelectWeapon();
            }
        }
    }



    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
