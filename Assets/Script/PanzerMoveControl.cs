using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerMoveControl : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    private float backSpeed;
    [SerializeField] float rotateSpeed;

    private Vector3 velocity;

    void Start()
    {
        backSpeed = forwardSpeed / 2;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        PanzerMove();
    }

    private void PanzerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertiacl = Input.GetAxis("Vertical");

        velocity = new Vector3(0, 0, vertiacl);

        velocity = transform.TransformDirection(velocity);

        if (vertiacl > 0.1)
        {
            velocity *= forwardSpeed;
        }
        else if ((vertiacl < -0.1)) 
        {
            velocity *= backSpeed;
            horizontal *= -1f;
        }

        transform.localPosition += velocity * Time.fixedDeltaTime;
        transform.Rotate(0, horizontal * rotateSpeed, 0);
    }
}