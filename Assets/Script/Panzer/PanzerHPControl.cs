using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerHPControl : MonoBehaviour
{
    [SerializeField] GameObject headState;
    [SerializeField] GameObject forwardState;
    [SerializeField] GameObject backState;
    [SerializeField] GameObject rightState;
    [SerializeField] GameObject leftState;

    [SerializeField] Material Green;
    [SerializeField] Material Yellow;
    [SerializeField] Material Red;
    [SerializeField] Material Black;
    [SerializeField] Material DeepGreen;
    [SerializeField] Material DeepYellow;
    [SerializeField] Material DeepRed;
    [SerializeField] Material DeepBlack;

    [SerializeField] int headHP;
    [SerializeField] int forwardHP;
    [SerializeField] int backHP;
    [SerializeField] int rightHP;
    [SerializeField] int leftHP;
    [SerializeField] int coreHP;

    void Start()
    {
        
    }

    void Update()
    {
        HeadStateUpdate(headHP);
        bodyStateUpdate(forwardState, forwardHP);
        bodyStateUpdate(backState, backHP);
        bodyStateUpdate(rightState, rightHP);
        bodyStateUpdate(leftState, leftHP);

    }

    private void bodyStateUpdate(GameObject parts, int nowHp)
    {
        switch (nowHp)
        {
            case 0:
                parts.GetComponent<MeshRenderer>().material.color = DeepBlack.color;
                break;
            case 1:
                parts.GetComponent<MeshRenderer>().material.color = DeepRed.color;
                break;
            case 2:
                parts.GetComponent<MeshRenderer>().material.color = DeepYellow.color;
                break;
            case 3:
                parts.GetComponent<MeshRenderer>().material.color = DeepGreen.color;
                break;
        }
    }

    private void HeadStateUpdate(int nowHp)
    {
        switch (nowHp)
        {
            case 0:
                headState.GetComponent<MeshRenderer>().material.color = Black.color;
                break;
            case 1:
                headState.GetComponent<MeshRenderer>().material.color = Red.color;
                break;
            case 2:
                headState.GetComponent<MeshRenderer>().material.color = Yellow.color;
                break;
            case 3:
                headState.GetComponent<MeshRenderer>().material.color = Green.color;
                break;
        }
    }

    public void HeadGetDamage(int Damage)
    {
        headHP -= Damage;

        if (headHP < 0)
        {
            headHP = 0;
            CoreGetDamage();
        }

        HeadStateUpdate(headHP);
    }

    public void ForwardGetDamage(int Damage)
    {
        forwardHP -= Damage;

        if (forwardHP < 0)
        {
            forwardHP = 0;
            CoreGetDamage();
        }

        bodyStateUpdate(forwardState, forwardHP);
    }

    public void BackGetDamage(int Damage)
    {
        backHP -= Damage;

        if (backHP < 0)
        {
            backHP = 0;
            CoreGetDamage();
        }

        bodyStateUpdate(backState, backHP);
    }

    public void RightGetDamage(int Damage)
    {
        rightHP -= Damage;

        if (rightHP < 0)
        {
            rightHP = 0;
            CoreGetDamage();
        }

        bodyStateUpdate(rightState, rightHP);
    }

    public void LeftGetDamage(int Damage)
    {
        leftHP -= Damage;

        if (leftHP < 0)
        {
            leftHP = 0;
            CoreGetDamage();
        }

        bodyStateUpdate(leftState, leftHP);
    }

    private void CoreGetDamage()
    {
        coreHP--;

        if(coreHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
