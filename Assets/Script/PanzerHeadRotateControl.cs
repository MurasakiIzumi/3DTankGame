using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerHeadRotateControl : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] float rotateSpeed;
    [SerializeField] CameraChangeControl cameraChange;
    [SerializeField] float viewRange;
    [SerializeField] GameObject GunSystem;

    private bool isZoom;
    void Start()
    {
        isZoom = false;
    }

    void Update()
    {
        isZoom = cameraChange.GetisZoom();
    }

    void FixedUpdate()
    {
        if (!isZoom)
        {
            HeadRotateMain();
        }
        else
        {
            HeadRotateSub();
        }
    }

    private void HeadRotateMain()
    {
        GunSystem.transform.localEulerAngles = Vector3.zero;

        Vector3 target=mainCamera.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rotateSpeed * Time.fixedDeltaTime);
    }

    private void HeadRotateSub()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        float maxLimit = viewRange;
        float minLimit = 360 - maxLimit;

        var localAngle = GunSystem.transform.localEulerAngles;
        localAngle.x -= MouseY;
        if ((localAngle.x > maxLimit) && (localAngle.x < 180))
        {
            localAngle.x = maxLimit;
        }
        if ((localAngle.x < minLimit) && (localAngle.x > 180))
        {
            localAngle.x = minLimit;
        }
        GunSystem.transform.localEulerAngles = localAngle;

        if (Mathf.Abs(MouseX) > 0.001f)
        {
            transform.RotateAround(transform.position, Vector3.up, MouseX);
        }
    }
}
