using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dropSpeed;
    [SerializeField] int Damage;
    [Header("—Ž‰ºŠJŽnŽžŠÔ")][SerializeField] float rangeTime;
    //[Header("–½’†")] public GameObject Spark;

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

    public void Hit()
    {
        //Instantiate(Spark, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
