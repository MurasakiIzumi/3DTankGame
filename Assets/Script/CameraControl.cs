using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]  GameObject Panzer;
    [SerializeField] bool isMainCamera;
    [SerializeField] CameraChangeControl cameraChange;
    [SerializeField]  float SmoothTime = 0.3f;
    [Header("Panzer RotateSpeed")][SerializeField] float rotateSpeed;

    private Vector3 distance;
    private Vector3 Velocity = Vector3.zero;
    private bool isZoom;

    void Start()
    {
        distance = transform.position - Panzer.transform.position;
        isZoom = false;
    }

    void Update()
    {
        isZoom = cameraChange.GetisZoom();
    }

    void FixedUpdate()
    {
        if (isMainCamera)
        {
            CameraRotate();
        }
        else
        {
            SmoothMove();
        }
    }

    private void SmoothMove()
    {
        Vector3 targetPosition = Panzer.transform.position + distance;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), ref Velocity, SmoothTime);

        smoothPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, Time.deltaTime * SmoothTime * 10);

        transform.position = smoothPosition;
    }

    private void CameraRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");

        if (Mathf.Abs(MouseX) > 0.001f)
        {
            transform.RotateAround(Panzer.transform.position, Vector3.up, MouseX);
        }

        if (isZoom)
        {
            MouseX= Input.GetAxis("Horizontal");

            if (Mathf.Abs(MouseX) > 0.001f)
            {
                transform.RotateAround(Panzer.transform.position, Vector3.up, MouseX * rotateSpeed);
            }
        }
    }
}
