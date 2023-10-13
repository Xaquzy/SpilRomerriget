using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbartestscript : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public Healthbar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage; 
        healthbar.SetHealth(currentHealth);
    }
}
