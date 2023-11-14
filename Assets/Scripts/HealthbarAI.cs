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
    [SerializeField] public float OppositionAttackRange;
    private float distanceToOther;
    public Animator PlayerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DamageOpposition();
    }


    // Player attack funktion
    public void DamageOpposition()
    {

        distanceToOther = Mathf.Abs((AI.position - player.position).magnitude);
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (distanceToOther < OppositionAttackRange)
            {
                {
                    currentHealth = currentHealth - damage;
                    healthbar.SetHealth(currentHealth);
                    PlayerAnimator.SetBool("Attack", true);
                }
            }
        }

        else
        {
            PlayerAnimator.SetBool("Attack", false);
        }
    }

}
