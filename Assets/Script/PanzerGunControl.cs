using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerGunControl : MonoBehaviour
{
    [SerializeField] Transform CreatePoint;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Gun;
    [SerializeField] float coolTime;

    private float timer_cooltime;
    private float animationSpeed;
    private Vector3 defultPos;


    void Start()
    {
        timer_cooltime = coolTime;
        defultPos=Gun.transform.localPosition;
        animationSpeed = 3f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shot();
        }

        Animation();
        Timer();
    }

    private void Timer()
    {
        if (timer_cooltime > coolTime)
        {
            timer_cooltime = coolTime;
            Gun.transform.localPosition = defultPos;
        }
        else 
        {
            timer_cooltime += Time.deltaTime;
        }
    }

    private void Shot()
    {
        if (timer_cooltime < coolTime)
        {
            return;
        }

        SetBullet();

        timer_cooltime = 0;
    }

    private void Animation()
    {
        if (timer_cooltime >= coolTime)
        {
            Gun.transform.localPosition = defultPos;
        }
        else if (timer_cooltime > coolTime / 2)
        {
            Gun.transform.localPosition += Vector3.forward * animationSpeed * Time.deltaTime;
        }
        else if (timer_cooltime < coolTime / 2)
        {
            Gun.transform.localPosition += Vector3.back * animationSpeed * Time.deltaTime;
        }

    }

    private void SetBullet()
    {
        Instantiate(Bullet, CreatePoint.transform.position, transform.rotation);
        //Instantiate(GunSpark, GunSparkPos.position, transform.rotation);
    }
}
