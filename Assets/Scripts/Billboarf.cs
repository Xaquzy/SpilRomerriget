using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarf : MonoBehaviour
{
    public Transform mainCam;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCam.forward);
    }
}
