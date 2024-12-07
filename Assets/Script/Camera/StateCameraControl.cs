using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCameraControl : MonoBehaviour
{
    private GameObject Target;

    void Start()
    {
        Target = GameObject.FindWithTag("StateCenter");
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPos = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
        transform.position = targetPos;
    }
}
