using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PanzerHeadRotateControl : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] float rotateSpeed;

    void Start()
    {
    }

    void FixedUpdate()
    {
        HeadRotate();
    }

    private void HeadRotate()
    {
        Vector3 target=mainCamera.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), rotateSpeed * Time.fixedDeltaTime);
    }
}
