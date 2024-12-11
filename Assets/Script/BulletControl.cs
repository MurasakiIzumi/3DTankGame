using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dropSpeed;
    [SerializeField] int apLv;
    [SerializeField] int Damage;
    [SerializeField] float baseHitRate;
    [SerializeField] float rangeTime;
    [SerializeField] GameObject missSpark1;
    [SerializeField] GameObject missSpark2;
    [SerializeField] GameObject hitSpark;

    private float timer_range;
    private float HitRate;

    void Start()
    {
        timer_range = 0;
        HitRate = baseHitRate;
    }

    void Update()
    {
        MoveForward();
        Drop();
    }

    private void MoveForward()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Drop()
    {
        timer_range += Time.deltaTime;

        if (timer_range > rangeTime)
        {
            transform.position += Vector3.down * dropSpeed * Time.deltaTime;
        }
    }

    private void HitGround()
    {
        Instantiate(missSpark1, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void HitWall()
    {
        Destroy(gameObject);
    }

    private void HitTarget()
    {
        Instantiate(hitSpark, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void MissTarget()
    {
        Instantiate(missSpark2, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            HitGround();
        }

        if (collision.gameObject.CompareTag ("Wall"))
        {
            HitWall();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (apLv <= collision.gameObject.GetComponent<PanzerHPControl>().GetDefLv())
            {
                HitRate = baseHitRate / 2;
            }

            if (Random.Range(1, 101) < HitRate)
            {
                if (collision.collider.gameObject.GetComponent<PanzerCollision>())
                {
                    collision.collider.gameObject.GetComponent<PanzerCollision>().GetDamage(1);
                }
                else
                {
                    MissTarget();
                    return;
                }
                
                HitTarget();
            }
            else
            {
                MissTarget();
            }
        }

        if (collision.gameObject.CompareTag("NPC"))
        {
            if (apLv <= collision.gameObject.GetComponent<NPCHPControl>().GetDefLv())
            {
                HitRate = baseHitRate / 2;
            }

            if (Random.Range(1, 101) < HitRate)
            {
                if (collision.collider.gameObject.GetComponent<NPCCollision>())
                {
                    collision.collider.gameObject.GetComponent<NPCCollision>().GetDamage(1);
                }
                else
                {
                    MissTarget();
                    return;
                }

                HitTarget();
            }
            else
            {
                MissTarget();
            }
        }
    }
}
