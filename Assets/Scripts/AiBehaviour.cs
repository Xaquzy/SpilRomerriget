using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public class AiBehaviour : MonoBehaviour
{
    //Definitioner
    [SerializeField] private Transform player;
    public NavMeshAgent AI;
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 3;
    public float targetRadius = 0.1f;
    [SerializeField] private float maxChaseDistance;
    [SerializeField] public float attackRange;


    private int indexOfTarget;
    private Vector3 targetPoint;
    private float distanceToPlayer;
    private State state = State.PatrolState;
    private CharacterController controller;
    [SerializeField] private float timeToSpeedUp;
    [SerializeField] private float timeToRotate;
    private float currentSpeed = 3;
    private Vector3 oldRotation;
    private Vector3 targetRotation;
    private float speedUpTimer;
    private float rotateTimer = 0f;
    private bool InAttackRange;
   



    // Start is called before the first frame update
    void Start()
    {
        LookAtTarget();
        controller = GetComponent<CharacterController>();
        indexOfTarget = -1;
        NextTarget();
 
    }



    // Update is called once per frame
    void Update()
    {
        if (state == State.PatrolState)
        {
            Patrol();
        }
        if (state == State.ChaseState)
        {
            Chase();
        }
        if (state == State.AttackState)
        {
            Attack();
        }      
    }



    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % points.Length;
        targetPoint = points[indexOfTarget].position;
        targetPoint.y = transform.position.y;
    }

    

    void LookAtTarget()
    {
        if (state == State.PatrolState)
        {
            Vector3 lookAt = targetPoint;
            lookAt.y = transform.position.y;

            Vector3 lookDir = (lookAt - transform.position).normalized;
            transform.forward = lookDir;
        }
        else
        {
            Vector3 lookAt = player.transform.position;
            lookAt.y = transform.position.y;

            Vector3 lookDir = (lookAt - transform.position).normalized;
            transform.forward = lookDir;
        }

    }

    void SetRotation()
    {
        if (rotateTimer < timeToRotate)
        {
            float t = rotateTimer / timeToRotate;
            transform.forward = Vector3.Slerp(oldRotation, targetRotation, t);
            rotateTimer += Time.deltaTime;

        }
    }


    void SetSpeed()
    {
        if (speedUpTimer < timeToSpeedUp)
        {
            float t = speedUpTimer / timeToSpeedUp;
            currentSpeed = Mathf.Lerp(0f, moveSpeed, t);
            speedUpTimer += Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }
 


    enum State
    {
        PatrolState,
        ChaseState,
        AttackState
    }
    void Patrol()
    {
        Debug.Log("DTP " + distanceToPlayer + ": Patrolling");

        LookAtTarget();


        if ((transform.position - targetPoint).magnitude < targetRadius)
        {
            NextTarget();
            LookAtTarget();
            speedUpTimer = 0f;
        }


        Vector3 velocity = targetPoint - transform.position;
        velocity.Normalize();
        velocity *= moveSpeed * Time.deltaTime;
        controller.Move(velocity);

        SetRotation();
        SetSpeed();


        distanceToPlayer = (AI.transform.position - player.position).magnitude;
        if (distanceToPlayer < maxChaseDistance)
        {
            state = State.ChaseState;
        }

    }

    // Chase funktion
    void Chase()
    {
        Debug.Log("DTP " + distanceToPlayer + ": Chasing");
        AI.speed = moveSpeed;
        AI.SetDestination(player.position);        
        distanceToPlayer = (AI.transform.position - player.position).magnitude;
        if (distanceToPlayer > maxChaseDistance)
        {
            state = State.PatrolState;
        }

        if (distanceToPlayer < attackRange)
        {
            state = State.AttackState;
        }

       
    }
    public void Attack()
        {
        Debug.Log("DTP " + distanceToPlayer + ": Attacking");
        
        distanceToPlayer = (AI.transform.position - player.position).magnitude;
        if (distanceToPlayer > attackRange)
        {
            state = State.ChaseState;
        }
    }

}
