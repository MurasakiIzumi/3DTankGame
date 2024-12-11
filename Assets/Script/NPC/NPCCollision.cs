using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    [SerializeField] NPCHPControl hpControl;

    public void GetDamage(int damage)
    {
        if (gameObject.CompareTag("NPCHeadCollision"))
        {
            hpControl.HeadGetDamage(damage);
        }
        else if (gameObject.CompareTag("NPCFowCollision"))
        {
            hpControl.ForwardGetDamage(damage);
        }
        else if (gameObject.CompareTag("NPCBackCollision"))
        {
            hpControl.BackGetDamage(damage);
        }
        else if (gameObject.CompareTag("NPCRightCollision"))
        {
            hpControl.RightGetDamage(damage);
        }
        else if (gameObject.CompareTag("NPCLeftCollision"))
        {
            hpControl.LeftGetDamage(damage);
        }
        else if (gameObject.CompareTag("Armour"))
        {
            Destroy(gameObject);
        }
    }
}
