using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVchange : MonoBehaviour
{
    public GameObject FirstCamera;
    public GameObject ThirdCamera;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FirstCamera.SetActive(!FirstCamera.activeSelf);
            ThirdCamera.SetActive(!ThirdCamera.activeSelf);
        }
    }
}