using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanzerSupplyControl : MonoBehaviour
{
    [SerializeField] private PanzerGunControl gunControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Supply"))
        {
            gunControl.SupplyIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Supply"))
        {
            gunControl.SupplyOut();
        }
    }
}
