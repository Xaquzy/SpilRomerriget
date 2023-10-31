using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVchange : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject Thirdcamera;
    void Start()
    {
        
    }

    /*public void ChangePOV()
    {
        if
        {
            MainCamera.enabled = false;
            Thirdcamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            Thirdcamera.enabled = false;
        }
    }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("adhgadkhfg");
            MainCamera.SetActive(!MainCamera.activeSelf);
            Thirdcamera.SetActive(!Thirdcamera.activeSelf);
            //ChangePOV();
        }
        //else 
        //{
        //    MainCamera.SetActive(true);
        //    Thirdcamera.SetActive(false);
        //}
    }
}