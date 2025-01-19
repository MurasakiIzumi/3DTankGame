using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRadarCobtrol : MonoBehaviour
{
    [SerializeField] NPCGunControl npcGunControl;
    [SerializeField] NPCMoveControl npcMoveControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            npcGunControl.SetTarget(other.gameObject);
            npcMoveControl.Engage();
            gameObject.SetActive(false);
        }

        //if (other.gameObject.tag == "NPC")
        //{
        //    npcGunControl.SetTarget(other.gameObject);
        //
        //    gameObject.SetActive(false);
        //}
    }
}
