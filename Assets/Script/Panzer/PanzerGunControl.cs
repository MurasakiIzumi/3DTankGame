using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanzerGunControl : MonoBehaviour
{
    [SerializeField] Transform CreatePoint;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject GunSpark;
    [SerializeField] GameObject Gun;

    [SerializeField] TextMeshProUGUI NowAmmoUI;
    [SerializeField] TextMeshProUGUI AllAmmoUI;

    [SerializeField] AudioClip relordSE;
    [SerializeField] AudioClip noammoSE;

    [SerializeField] float coolTime;
    [SerializeField] float relordTime;
    [SerializeField] int ammoMax;
    [SerializeField] int ammoOnce;

    private float timer_cooltime;
    private float timer_relordtime;
    private float animationSpeed;
    private Vector3 defultPos;
    private Vector3 sparkPos;

    private int allAmmo;
    private int nowAmmo;
    private bool reLording;
    private bool noAmmo;

    private AudioSource audioSource;

    void Start()
    {
        timer_cooltime = coolTime;
        timer_relordtime = 0f;
        defultPos =Gun.transform.localPosition;
        animationSpeed = 4f;

        allAmmo = ammoMax;
        nowAmmo = ammoOnce;
        reLording = false;
        noAmmo = false;
        AmmoUIUpdate();

        audioSource=GetComponent<AudioSource>();
        audioSource.clip = relordSE;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shot();
        }

        Relord();
        Animation();
        Timer();
    }

    private void Timer()
    {
        if (timer_cooltime >= coolTime)
        {
            timer_cooltime = coolTime;
            Gun.transform.localPosition = defultPos;
        }
        else 
        {
            timer_cooltime += Time.deltaTime;
        }

        if (reLording)
        {
            if (timer_relordtime >= relordTime)
            {
                timer_relordtime=relordTime;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                timer_relordtime+=Time.deltaTime;
            }
        }
    }

    private void Shot()
    {
        if (reLording)
        {
            return;
        }
        if (timer_cooltime < coolTime)
        {
            return;
        }
        if (noAmmo)
        {
            timer_cooltime = 0;
            audioSource.PlayOneShot(noammoSE);
            return;
        }

        SetBullet();

        timer_cooltime = 0;
        nowAmmo--;
        AmmoUIUpdate();

        if (nowAmmo <= 0)
        {
            reLording = true;

            if (allAmmo <= 0)
            {
                reLording = false;
                noAmmo = true;
            }
        }
    }

    private void Relord()
    {
        if (!reLording)
        {
            return;
        }
        if (allAmmo == 0)
        {
            return;
        }
        if (audioSource.time < audioSource.clip.length)
        {
            return;
        }


        nowAmmo = ammoOnce;
        allAmmo -= ammoOnce;
        AmmoUIUpdate();

        timer_relordtime = 0;
        reLording = false;
    }

    private void Animation()
    {
        if (noAmmo)
        {
            Gun.transform.localPosition = defultPos;
            return;
        }

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
        sparkPos = CreatePoint.transform.position + CreatePoint.transform.forward * 0.2f;
        Instantiate(Bullet, CreatePoint.transform.position, transform.rotation);
        Instantiate(GunSpark, sparkPos, transform.rotation);
    }

    private void AmmoUIUpdate()
    {
        if (nowAmmo == 0)
        {
            NowAmmoUI.color = Color.black;
        }
        else if (nowAmmo <= ammoOnce / 5)
        {
            NowAmmoUI.color = Color.red;
        }
        else if (nowAmmo <= ammoOnce / 2)
        {
            NowAmmoUI.color = Color.yellow;
        }
        else
        {
            NowAmmoUI.color = Color.white;
        }

        if (allAmmo == 0)
        {
            AllAmmoUI.color = Color.black;
        }

        NowAmmoUI.text = nowAmmo.ToString();
        AllAmmoUI.text = allAmmo.ToString();
    }
}
