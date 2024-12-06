using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeControl : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera subCamera;

    private int zoom;

    void Start()
    {
        mainCamera.enabled = true;
        subCamera.enabled = false;

        zoom = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            zoom *= -1;
        }

        CameraChange();
    }

    private void CameraChange()
    {

        if (zoom > 0)
        {
            mainCamera.enabled = false;
            subCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            subCamera.enabled = false;
        }
    }
}
