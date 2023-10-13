using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PatrolAndChase : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float targetRadius = 0.1f;
    [SerializeField] private float minChaseDistance;
    [SerializeField] private float maxChaseDistance;
    [SerializeField] private float PlayerSpace;

    private int indexOfTarget;
    private Vector3 targetPoint;
    private float distanceToPlayer;
    private State state = State.PatrolState;
    private CharacterController controller;


    //Smooth patrol
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
        controller = GetComponent<CharacterController>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
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
            //Attack();
        }

        if ((transform.position - targetPoint).magnitude < targetRadius)
        { 
            NextTarget();
            LookAtTarget();
            speedUpTimer = 0f;
        }


        SetRotation();
        SetSpeed();
        Vector3 velocity = targetPoint - transform.position;
        velocity.Normalize();
        velocity *= currentSpeed * Time.deltaTime;
        controller.Move(velocity);


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
        if ((transform.position - targetPoint).magnitude < targetRadius)
        {
            NextTarget();
            LookAtTarget();
        }

        Vector3 velocity = targetPoint - transform.position;
        velocity.Normalize();
        velocity *= moveSpeed * Time.deltaTime;
        controller.Move(velocity);
        float distanceToPlayeer =
            (player.transform.position - transform.position).magnitude;

        distanceToPlayer = (transform.position - player.position).magnitude;
        if (distanceToPlayer < minChaseDistance)
        {
            state = State.ChaseState;
        }

    }

    void Chase()
    {
        Vector3 velocity = player.position - transform.position;
        velocity.Normalize();
        velocity *= moveSpeed * Time.deltaTime;
        controller.Move(velocity);
        Vector3 lookAt = player.position;
        Vector3 lookDir = (lookAt - transform.position).normalized;
        transform.forward = lookDir;

        Debug.Log(distanceToPlayer);

        if (distanceToPlayer > maxChaseDistance)
        {
            state = State.PatrolState;
        }

        if (distanceToPlayer <= PlayerSpace)
        {
            state = State.AttackState;
        }
    }
    void Attack()
        {
        
        Debug.Log("Attacking");
            Vector3 velocity = player.position - transform.position;
            velocity.Normalize();
            velocity *= moveSpeed * Time.deltaTime;
            controller.Move(velocity);
            Vector3 lookAt = player.position;
            Vector3 lookDir = (lookAt - transform.position).normalized;
            transform.forward = lookDir;

        }

}
