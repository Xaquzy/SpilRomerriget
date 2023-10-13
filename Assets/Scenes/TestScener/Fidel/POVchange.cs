using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVchange : MonoBehaviour
{

    public Camera MainCamera;
    public Camera Thirdcamera;
    void Start()
    {
        MainCamera.enabled = true;
        Thirdcamera.enabled = false;
    }

    public void ChangePOV()
    {
        if (MainCamera.enabled) 
        {
            MainCamera.enabled = false;
            Thirdcamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            Thirdcamera.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P)) 
        {
            ChangePOV();
        }
    }
}
