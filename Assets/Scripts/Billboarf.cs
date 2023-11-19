using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarf : MonoBehaviour
{
    public Transform mainCam;
    void Update()
    {
        transform.LookAt(transform.position + mainCam.forward);
    }
}
