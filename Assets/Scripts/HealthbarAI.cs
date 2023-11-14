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
    public Animator NpcAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        NpcAnimator.SetBool("LøveDød", false);
    }

    // Update is called once per frame
    void Update()
    {
        DamageOpposition();
        Død();
    }

    
    // Player angriber NPC funktion
    public void DamageOpposition()
    {

        distanceToOther = Mathf.Abs((AI.position - player.position).magnitude);


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            PlayerAnimator.SetBool("PlayerAttack", true);
            if (distanceToOther < OppositionAttackRange)
            {
                {
                    currentHealth = currentHealth - damage;
                    healthbar.SetHealth(currentHealth);
                }
            }
        }
        else
        {
            PlayerAnimator.SetBool("PlayerAttack", false);
        }

       
    }

    public void Død()
    {
        if (currentHealth == 0)
        {
            NpcAnimator.SetBool("LøveDød", true);
        }
    }


}
