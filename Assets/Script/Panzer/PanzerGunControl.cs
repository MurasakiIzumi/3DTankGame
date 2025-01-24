using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanzerGunControl : MonoBehaviour
{
    [SerializeField] Transform CreatePoint;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject GunSpark;
    [SerializeField] GameObject Gun;

    [SerializeField] AudioClip relordSE;
    [SerializeField] AudioClip noammoSE;

    [SerializeField] float shotSpread;

    [SerializeField] float coolTime;
    [SerializeField] float relordTime;
    [SerializeField] int ammoMax;
    [SerializeField] int ammoOnce;
    [SerializeField] int supplyAmmo;

    [SerializeField] float gunBackTimeLimit;
    [SerializeField] float gunBackSpeed;

    private float timer_cooltime;
    private float timer_relordtime;
    private Vector3 defultPos;
    private Vector3 sparkPos;

    private int allAmmo;
    private int nowAmmo;
    private bool reLording;
    private bool noAmmo;

    private bool inSupplyArea;

    private AudioSource audioSource;
    private Animator animator_sight;
    private Animator animator_supply;
    private TextMeshProUGUI NowAmmoUI;
    private TextMeshProUGUI AllAmmoUI;

    void Start()
    {
        timer_cooltime = coolTime;
        timer_relordtime = 0f;
        defultPos =Gun.transform.localPosition;

        audioSource =GetComponent<AudioSource>();
        audioSource.clip = relordSE;

        NowAmmoUI = GameObject.FindWithTag("NowAmmo").GetComponent<TextMeshProUGUI>();
        AllAmmoUI = GameObject.FindWithTag("AllAmmo").GetComponent<TextMeshProUGUI>();
        animator_sight = GameObject.FindWithTag("Sight").GetComponent<Animator>();
        animator_sight.speed = 1f / relordTime;
        animator_supply = GameObject.FindWithTag("SupplyUI").GetComponent<Animator>();
        animator_supply.gameObject.SetActive(false);

        allAmmo = ammoMax;
        nowAmmo = ammoOnce;
        reLording = false;
        noAmmo = false;
        AmmoUIUpdate();

        inSupplyArea = false;
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
        AmmoSupply();
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

        animator_sight.Play("Sight", -1, (1f - (float)nowAmmo / ammoOnce));

        if (nowAmmo <= 0)
        {
            if (allAmmo <= 0)
            {
                reLording = false;
                noAmmo = true;
            }
            else
            {
                reLording = true;
                animator_sight.Play("Relord", -1, 0f);
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

        if (timer_cooltime >= coolTime / gunBackTimeLimit)
        {
            Gun.transform.localPosition = defultPos;
        }
        else if (timer_cooltime > coolTime / gunBackTimeLimit / 2f)
        {
            Gun.transform.localPosition += Vector3.forward * gunBackSpeed * Time.deltaTime;
        }
        else if (timer_cooltime < coolTime / gunBackTimeLimit / 2f)
        {
            Gun.transform.localPosition += Vector3.back * gunBackSpeed * Time.deltaTime;
        }
    }

    private void SetBullet()
    {
        sparkPos = CreatePoint.transform.position + CreatePoint.transform.forward * 0.2f;
        Instantiate(Bullet, CreatePoint.transform.position, Quaternion.LookRotation(BulletSpread()));
        Instantiate(GunSpark, sparkPos, transform.rotation);
    }

    private Vector3 BulletSpread()
    {
        float spreadAngleRatio = shotSpread / 180f;
        Vector3 spreadDirection = Vector3.Slerp(CreatePoint.forward, Random.insideUnitSphere, spreadAngleRatio);
        return spreadDirection;
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

        NowAmmoUI.text = nowAmmo + "/" + ammoOnce;
        AllAmmoUI.text = allAmmo.ToString();
    }

    private void AmmoSupply()
    {
        if (!inSupplyArea)
        {
            return;
        }

        if (allAmmo >= ammoMax)
        {
            animator_supply.Play("Supply", 0, 0f);
            animator_supply.speed = 0f;
            animator_supply.gameObject.SetActive(false);
            return;
        }
        else
        {
            animator_supply.gameObject.SetActive(true);
            animator_supply.speed = 0.5f;
        }

        var state = animator_supply.GetCurrentAnimatorStateInfo(0);

        if (state.normalizedTime >= 1f) 
        {
            allAmmo += supplyAmmo;
            AmmoUIUpdate();

            if (allAmmo < ammoMax)
            {
                animator_supply.Play("Supply", 0, 0f);
            }
        }
    }

    public void SupplyIn()
    {
        inSupplyArea = true;
        animator_supply.gameObject.SetActive(true);
        animator_supply.Play("Supply", 0, 0f);
    }

    public void SupplyOut()
    {
        inSupplyArea = false;
        animator_supply.gameObject.SetActive(false);
    }
}
