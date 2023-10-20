using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarAI : MonoBehaviour
{
    public int maxHealth = 4;
    public int damage = 1;
    public int currentHealth;
    public HealthbarCanvas healthbar;
    [SerializeField] private Transform player;
    [SerializeField] private Transform AI;
    [SerializeField] public float PlayerAttackRange;
    private float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Damage();
    }



    public void Damage()
    {

        distanceToPlayer = (AI.position - player.position).magnitude;
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (distanceToPlayer < PlayerAttackRange)
            {
                {
                    currentHealth = currentHealth - damage;
                    healthbar.SetHealth(currentHealth);
                }
            }
        }
    }

}
