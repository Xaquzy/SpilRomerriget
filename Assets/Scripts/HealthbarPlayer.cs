using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthbarPlayer : MonoBehaviour
{
    public int maxHealth = 4;
    public int damage = 1;
    public int currentHealth;
    public HealthbarCanvas healthbar;
    [SerializeField] private Transform player;
    [SerializeField] private Transform AI;
    [SerializeField] public float OppositionAttackRange;
    private float distanceToOther;
    private float time = 0.0f;
    public float AttackFrekvens = 0.5f;
    public Animator NpcAnimator;
    // Start is called before the first frame update


    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        time = AttackFrekvens;
    }

    // Update is called once per frame
    void Update()
    {
        DamageOpposition();   
    }


    //NPC angriber player funktion
    public void DamageOpposition()
    {

        distanceToOther = Mathf.Abs((AI.position - player.position).magnitude);
        time = time + Time.deltaTime;
    
            if (distanceToOther < OppositionAttackRange)
            {

                if (time >= AttackFrekvens)
                {
                time = 0.0f;
                currentHealth = currentHealth - damage;
                healthbar.SetHealth(currentHealth);
                AI.GetComponent<NavMeshAgent>().speed = 0;
                NpcAnimator.SetBool("Attack", true);
                }

                NpcAnimator.SetBool("Attack", false);
            }
       
    }

}
