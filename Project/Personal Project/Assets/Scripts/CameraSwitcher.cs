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
        if (Input.GetKey(KeyCode.Q))
        {
            if (!isSecondaryActive)  
            {
                isSecondaryActive = true;
                mainCamera.enabled = false;
                secondaryCamera.enabled = true;
            }
        }
        else
        {
            if (isSecondaryActive)  
            {
                isSecondaryActive = false;
                mainCamera.enabled = true;
                secondaryCamera.enabled = false;
            }
        }
    }
}
