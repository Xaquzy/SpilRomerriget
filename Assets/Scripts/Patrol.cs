using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float targetRadius = 0.1f;
    [SerializeField] private float timeToSpeedUp;
    [SerializeField] private float timeToRotate;

    private int indexOfTarget;
    private float currentSpeed = 3;
    private Vector3 targetPoint;
    private Vector3 oldRotation;
    private Vector3 targetRotation;
    private float speedUpTimer;
    private float rotateTimer = 0f;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
    }
    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % points.Length;
        targetPoint = points[indexOfTarget].position;
        targetPoint.y = transform.position.y;
    }
   void SetSpeed()
    {
        if(speedUpTimer < timeToSpeedUp)
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

   
    void LookAtTarget()
    {
        Vector3 lookAt = targetPoint;
        lookAt.y = transform.position.y;
        Vector3 lookDir = (lookAt - transform.position).normalized;
        targetRotation = lookDir;
        rotateTimer = 0f;
    }

    void SetRotation()
    {
        if(rotateTimer < timeToRotate)
        {
            float t = rotateTimer / timeToRotate;
            transform.forward = Vector3.Slerp(transform.forward, targetRotation, t);        
            rotateTimer = rotateTimer + Time.deltaTime;
        }

    }

    void Update()
    {
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
}
