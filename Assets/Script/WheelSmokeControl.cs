using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSmokeControl : MonoBehaviour
{
    [SerializeField] ParticleSystem smoke;

    public void StartSmoke()
    {
        smoke.Play(true);
    }
}
