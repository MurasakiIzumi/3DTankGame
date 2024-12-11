using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveControl : MonoBehaviour
{
    [SerializeField] WheelSmokeControl smokeR;
    [SerializeField] WheelSmokeControl smokeL;
    [SerializeField] float forwardSpeed;
    [SerializeField] float rotateSpeed;

    private float backSpeed;
    private Vector3 velocity;
    private AudioSource audioSource;

    void Start()
    {
        backSpeed = forwardSpeed / 2;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.05f;
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
        float horizontal = 0f;
        float vertiacl = 1f;

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

        if ((horizontal == 0) && (vertiacl == 0))
        {
            WheelSmokeOn();
        }

        if ((horizontal != 0) || (vertiacl != 0))
        {
            audioSource.volume += Time.fixedDeltaTime / 5f;
            audioSource.volume = Mathf.Min(audioSource.volume, 0.2f);
        }
        else
        {
            audioSource.volume -= Time.fixedDeltaTime / 5f;
            audioSource.volume = Mathf.Max(audioSource.volume, 0.1f);
        }
    }

    private void WheelSmokeOn()
    {
        smokeR.StartSmoke();
        smokeL.StartSmoke();
    }
}
