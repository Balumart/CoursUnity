using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float cooldownSpeed;
    public float fireRate;
    public float recoilCooldown;
    private float accuracy;
    public float maxSpreadAngle;
    public float timeTillMaxSpread;

    public AudioSource gunshot;

    public AudioClip singleShot;

    public GameObject bullet;

    public GameObject shootPoint;

    public Animator animator;


    void Update()
    {
        //gestion tir, cadence de tir, recul, animations
        cooldownSpeed += Time.deltaTime * 60f;

        if (Input.GetButton("Fire1"))
        {
            animator.Play("shoot");
            accuracy += Time.deltaTime * 60f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                gunshot.PlayOneShot(singleShot);
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }
        }
        else
        {
            animator.Play("idle");
            recoilCooldown -= Time.deltaTime;
            if (recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }

        }
    }

    void Shoot()
    {
        RaycastHit hit;

        Quaternion fireRotation = Quaternion.LookRotation(transform.forward);

        //gestion du spread des balles
        float currentSpread = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);

        fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0.0f, currentSpread));

        if (Physics.Raycast(transform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, fireRotation);
            tempBullet.GetComponent<MoveBullet>().hitPoint = hit.point;
        }
    }
}
