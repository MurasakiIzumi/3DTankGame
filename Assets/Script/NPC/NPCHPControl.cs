using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHPControl : MonoBehaviour
{
    [SerializeField] int defLv;
    [SerializeField] int headHP;
    [SerializeField] int forwardHP;
    [SerializeField] int backHP;
    [SerializeField] int rightHP;
    [SerializeField] int leftHP;
    [SerializeField] int coreHP;

    [SerializeField] GameObject explosion;

    private GameControl gameControl;

    private void Start()
    {
        gameControl = GameObject.FindWithTag("EventSystem").GetComponent<GameControl>();
        gameControl.AddEnemy();
    }

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
            Debug.Log(1);
            Instantiate(explosion, transform.position, Quaternion.identity);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        gameControl.EnemyDestroy();
        Destroy(gameObject);
    }

    public void Engage()
    {
        gameControl.GetComponent<NPCMoveControl>().Engage();
    }

    public int GetDefLv()
    {
        return defLv; ;
    }
}
