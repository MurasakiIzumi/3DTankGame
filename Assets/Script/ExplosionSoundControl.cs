using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundControl : MonoBehaviour
{
    [SerializeField] AudioClip SE1;
    [SerializeField] AudioClip SE2;
    [SerializeField] AudioClip SE3;

    private int seIndex;
    private AudioSource audioSource;

    void Start()
    {
        seIndex = Random.Range(1, 4);
        audioSource = GetComponent<AudioSource>();

        switch (seIndex)
        {
            case 1:
                audioSource.clip = SE1;
                break;
            case 2:
                audioSource.clip = SE2;
                break;
            case 3:
                audioSource.clip = SE3;
                break;
        }

        audioSource.Play();
    }
}
