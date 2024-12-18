using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;       
    public Camera secondaryCamera; 
    private bool isSecondaryActive = false; 

    void Start()
    {
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            isSecondaryActive = !isSecondaryActive;

            mainCamera.enabled = !isSecondaryActive;
            secondaryCamera.enabled = isSecondaryActive;
        }
    }
}
