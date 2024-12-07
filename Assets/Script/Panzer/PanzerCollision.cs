using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerCollision : MonoBehaviour
{
    private PanzerHPControl hpControl;

    void Start()
    {
        hpControl = GameObject.FindWithTag("Player").GetComponent<PanzerHPControl>();    
    }

    public void GetDamage(int damage)
    {
        if (gameObject.tag == ("PLHeadCollision"))
        {
            hpControl.HeadGetDamage(damage);
        }
        else if (gameObject.tag == ("PLFowCollision"))
        {
            hpControl.ForwardGetDamage(damage);
        }
        else if(gameObject.tag == ("PLBackCollision"))
        {
            hpControl.BackGetDamage(damage);
        }
        else if(gameObject.tag == ("PLRightCollision"))
        {
            hpControl.RightGetDamage(damage);
        }
        else if(gameObject.tag == ("PLLeftCollision"))
        {
            hpControl.LeftGetDamage(damage);
        }
    }

}
