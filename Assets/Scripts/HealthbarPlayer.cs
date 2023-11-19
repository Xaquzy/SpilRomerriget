using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public Animator PlayerAnimator;
    public float countdownTime = 3f;
    private bool countdownStarted = false;

    // Start is called before the first frame update


    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        time = AttackFrekvens;
        PlayerAnimator.SetBool("PlayerDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        DamageOpposition();
        Dead();

        if (countdownStarted)
        { 
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0f)
                {
                    SceneManager.LoadScene("GameOver");
                }
            
        }

    }


    //NPC angriber player funktion
    public void DamageOpposition()
    {
        distanceToOther = Mathf.Abs((AI.position - player.position).magnitude);
        time = time + Time.deltaTime;
        if (distanceToOther < OppositionAttackRange)
        {
            NpcAnimator.SetBool("Attack", false);
            if (time >= AttackFrekvens)
            {
                time = 0.0f;
                currentHealth = currentHealth - damage;
                healthbar.SetHealth(currentHealth);
                AI.GetComponent<NavMeshAgent>().speed = 0;
                NpcAnimator.SetBool("Attack", true);
            }

        }
    }


    public void Dead()
    {
        if (currentHealth == 0)
        {
            if (!countdownStarted)
            {
                PlayerAnimator.SetBool("PlayerDead", true);
                countdownStarted = true;
            }
        }
    }
}
