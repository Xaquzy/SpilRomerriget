using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float visionRange = 10;
    [SerializeField] private float visionConeAngle = 30;
    [SerializeField] private TextMeshPro stateIndicator;

    private State state = State.Idle;

    float GetDistanceToPlayer()
    {
        return
            (player.transform.position - transform.position)
            .magnitude;
    }

    float GetAngleToPlayer()
    {
        Vector3 directionToPlayer =
            (player.transform.position - transform.position)
            .normalized;
        return Vector3.Angle(transform.forward, directionToPlayer);
    }

    bool SightLineObstructed()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        Ray ray = new Ray(
            transform.position,
            vectorToPlayer);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, vectorToPlayer.magnitude))
        {
            GameObject obj = hitInfo.collider.gameObject;
            return obj != player;
        }
        return false;
    }

    bool CanSeePlayer()
    {
        if(GetDistanceToPlayer() < visionRange)
        {
            if (GetAngleToPlayer() < visionConeAngle)
            {
                if (SightLineObstructed() == false)
                {
                    return true;
                }     
            }  
        }   
        return false;
    }
 

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Alert:
                Alert();
                break;
        }
    }

    void Idle()
    {
        if(CanSeePlayer())
        {
            state = State.Alert;
        }

        stateIndicator.text = "Idle...";
        transform.forward = Vector3.forward;
    }

    void Alert()
    {
        if (!CanSeePlayer())
        {
            state = State.Idle;
        }

        stateIndicator.text = "Alert!";
        transform.LookAt(player.transform);
    }

    enum State
    {
        Idle,
        Alert
    }
}
