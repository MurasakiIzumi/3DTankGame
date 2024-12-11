using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCameraControl : MonoBehaviour
{
    private GameObject posTarget;

    void Start()
    {
        posTarget = GameObject.FindWithTag("Player");
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
        Vector3 targetRotate = new Vector3(90f, 0f, -posTarget.transform.eulerAngles.y);
        transform.localEulerAngles = targetRotate;

    }
}
