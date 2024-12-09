using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    private NPCHPControl hpControl;
    void Start()
    {
        hpControl = GameObject.FindWithTag("NPC").GetComponent<NPCHPControl>();
    }

    public void GetDamage(int damage)
    {
        if (gameObject.tag == ("NPCHeadCollision"))
        {
            hpControl.HeadGetDamage(damage);
        }
        else if (gameObject.tag == ("NPCFowCollision"))
        {
            hpControl.ForwardGetDamage(damage);
        }
        else if (gameObject.tag == ("NPCBackCollision"))
        {
            hpControl.BackGetDamage(damage);
        }
        else if (gameObject.tag == ("NPCRightCollision"))
        {
            hpControl.RightGetDamage(damage);
        }
        else if (gameObject.tag == ("NPCLeftCollision"))
        {
            hpControl.LeftGetDamage(damage);
        }
        else if (gameObject.tag == ("Armour"))
        {
            Destroy(gameObject);
        }
    }
}
