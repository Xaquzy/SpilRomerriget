using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarf : MonoBehaviour
{
    public Transform mainCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.forward);
    }
}
