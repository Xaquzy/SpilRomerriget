using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVchange : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject ThirdCamera;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MainCamera.SetActive(!MainCamera.activeSelf);
            ThirdCamera.SetActive(!ThirdCamera.activeSelf);
        }
    }
}