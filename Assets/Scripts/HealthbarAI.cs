using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float countdownTime = 3f;
    private bool countdownStarted = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        NpcAnimator.SetBool("NpcDead", false);
    }

    void Update()
    {
        DamageOpposition();
        Dead();

        if (countdownStarted)
        {
            Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);

            if (SceneManager.GetActiveScene().name == "Bane 3")
            {
                countdownTime -= Time.deltaTime;
                if (countdownTime <= 0f)
                {
                    SceneManager.LoadScene("WIN");
                }
            }
            if (SceneManager.GetActiveScene().name == "Bane 2")
            {
                countdownTime -= Time.deltaTime;
                if (countdownTime <= 0f)
                {
                    SceneManager.LoadScene("2NextLevel");
                }
            }
            if (SceneManager.GetActiveScene().name == "Bane 1")
            {
                countdownTime -= Time.deltaTime;
                if (countdownTime <= 0f)
                {
                    SceneManager.LoadScene("1NextLevel");
                }
            }
        }
    }

    public void DamageOpposition()
    {
        distanceToOther = Mathf.Abs((AI.position - player.position).magnitude);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            PlayerAnimator.SetBool("PlayerAttack", true);
            if (distanceToOther < OppositionAttackRange)
            {
                currentHealth -= damage;
                healthbar.SetHealth(currentHealth);
            }
        }
        else
        {
            PlayerAnimator.SetBool("PlayerAttack", false);
        }
    }

    public void Dead()
    {
        if (currentHealth <= 0)
        {
            if (!countdownStarted)
            {
                NpcAnimator.SetBool("NpcDead", true);
                countdownStarted = true;
            }
        }
    }
}

