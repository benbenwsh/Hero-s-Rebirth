using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript1 : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth((maxHealth));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

    }
}


