using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dropSpeed;
    [SerializeField] int Damage;
    [Header("—Ž‰ºŠJŽnŽžŠÔ")][SerializeField] float rangeTime;
    [Header("ŠO‚ê")][SerializeField] GameObject missSpark1;
                    [SerializeField] GameObject missSpark2;
    [Header("–½’†")][SerializeField] GameObject hitSpark;

    private float timer_range;

    void Start()
    {
        timer_range = 0;
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
        if (collision.gameObject.tag == "Ground")
        {
            HitGround();
        }

        if (collision.gameObject.tag == "Wall")
        {
            HitWall();
        }

        if (collision.gameObject.tag == "Player")
        {
            if (Random.Range(1, 101) > 60)
            {
                collision.collider.gameObject.GetComponent<PanzerCollision>().GetDamage(1);
                HitTarget();
            }
            else
            {
                MissTarget();
            }
        }

        if (collision.gameObject.tag == "NPC")
        {
            if (Random.Range(1, 101) > 50)
            {
                collision.collider.gameObject.GetComponent<NPCCollision>().GetDamage(1);
                HitTarget();
            }
            else
            {
                MissTarget();
            }
        }
    }
}
