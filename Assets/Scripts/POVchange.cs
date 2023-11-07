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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("adhgadkhfg");
            MainCamera.SetActive(!MainCamera.activeSelf);
        }
    }
}