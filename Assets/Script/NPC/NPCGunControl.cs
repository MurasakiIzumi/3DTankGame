using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGunControl : MonoBehaviour
{
    [SerializeField] Transform CreatePoint;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject GunSpark;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform fowardPos;
    [SerializeField] GameObject searchArea;

    [SerializeField] float shotSpread;
    [SerializeField] float coolTime;
    [SerializeField] float searchDis;
    [SerializeField] float rotateSpeed;

    private float timer_cooltime;
    private float animationSpeed;
    private Vector3 defultPos;
    private Vector3 defultFoward;
    private Vector3 sparkPos;
    private Vector3 attackTarget;
    private bool isTargeting;

    private GameObject target;
    void Start()
    {
        timer_cooltime = coolTime / 8;
        defultPos = Gun.transform.localPosition;
        animationSpeed = 4f;
        isTargeting = false;
    }

    void FixedUpdate()
    {
        SearchAndFollow();
    }

    private void SearchAndFollow()
    {
        if (!target)
        {
            target = null;
            searchArea.SetActive(true);
            return;
        }

        attackTarget = target.transform.position;
        attackTarget.y = transform.position.y;
        defultFoward = fowardPos.position;
        defultFoward.y = transform.position.y;

        if (Vector3.Distance(transform.position, target.transform.position) < searchDis)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(attackTarget - transform.position), rotateSpeed * Time.deltaTime);
            isTargeting = true;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(defultFoward - transform.position), rotateSpeed * Time.deltaTime);
            isTargeting = false;
        }
    }

    void Update()
    {
        Shot();
        Animation();
        Timer();

        if (!target)
        {
            target = null;
            isTargeting = false;
        }
    }

    private void Timer()
    {
        if (!isTargeting)
        {
            return;
        }

        if (timer_cooltime >= coolTime)
        {
            timer_cooltime = coolTime;
            Gun.transform.localPosition = defultPos;
        }
        else
        {
            timer_cooltime += Time.deltaTime;
        }
    }

    private void Animation()
    {
        if (timer_cooltime >= coolTime / 8) 
        {
            Gun.transform.localPosition = defultPos;
        }
        else if (timer_cooltime > coolTime / 16)
        {
            Gun.transform.localPosition += Vector3.forward * animationSpeed * Time.deltaTime;
        }
        else if (timer_cooltime < coolTime / 16)
        {
            Gun.transform.localPosition += Vector3.back * animationSpeed * Time.deltaTime;
        }
    }

    private void Shot()
    {
        if (!isTargeting)
        {
            return;
        }
        if (timer_cooltime < coolTime)
        {
            return;
        }


        SetBullet();

        timer_cooltime = 0;
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

    public void SetTarget(GameObject Target)
    {
        target = Target;
    }
}
