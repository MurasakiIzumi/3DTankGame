using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCameraControl : MonoBehaviour
{
    private GameObject posTarget;
    private GameObject rotateTarget;

    void Start()
    {
        posTarget = GameObject.FindWithTag("Player");
        rotateTarget = GameObject.FindWithTag("PlayerHead");
    }
    void Update()
    {
        if (!posTarget)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        FollowTargetPos();
        FollowTargetRotate();
    }

    private void FollowTargetPos()
    {
        Vector3 targetPos = posTarget.transform.position;
        targetPos.y = transform.position.y;
        transform.position = targetPos;
    }

    private void FollowTargetRotate()
    {
        Vector3 targetRotate = new Vector3(90f, 0f, -rotateTarget.transform.localEulerAngles.y);
        transform.localEulerAngles = targetRotate;

    }
}
