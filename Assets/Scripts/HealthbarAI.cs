using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        NpcAnimator.SetBool("NpcDead", false);
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
                SceneManager.LoadScene("NextLevel");
            }
        }
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

    public void Dead()
    {
        if (currentHealth == 0)
        {
            if (!countdownStarted)
            {
                NpcAnimator.SetBool("NpcDead", true);
                countdownStarted = true;
            }
        }
    }
}
