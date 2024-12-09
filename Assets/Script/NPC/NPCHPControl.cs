using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class NPCHPControl : MonoBehaviour
{
    [SerializeField] int headHP;
    [SerializeField] int forwardHP;
    [SerializeField] int backHP;
    [SerializeField] int rightHP;
    [SerializeField] int leftHP;
    [SerializeField] int coreHP;

    [SerializeField] GameObject explosion;

    public void HeadGetDamage(int Damage)
    {
        headHP -= Damage;

        if (headHP < 0)
        {
            headHP = 0;
            CoreGetDamage();
        }
    }

    public void ForwardGetDamage(int Damage)
    {
        forwardHP -= Damage;

        if (forwardHP < 0)
        {
            forwardHP = 0;
            CoreGetDamage();
        }
    }

    public void BackGetDamage(int Damage)
    {
        backHP -= Damage;

        if (backHP < 0)
        {
            backHP = 0;
            CoreGetDamage();
        }
    }

    public void RightGetDamage(int Damage)
    {
        rightHP -= Damage;

        if (rightHP < 0)
        {
            rightHP = 0;
            CoreGetDamage();
        }
    }

    public void LeftGetDamage(int Damage)
    {
        leftHP -= Damage;

        if (leftHP < 0)
        {
            leftHP = 0;
            CoreGetDamage();
        }
    }

    private void CoreGetDamage()
    {
        coreHP--;

        if (coreHP <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
