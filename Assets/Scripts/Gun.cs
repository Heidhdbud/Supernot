using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("BulletPooling")]
    private List<GameObject> poolBullets = new List<GameObject>();
    protected int amountToPool;
    protected GameObject bulletPrefabs;

    [Header("Shooting")]
    protected Transform shootPos;
    protected float shootForce;
    protected float shootDelay;
    protected float timeSinceLastShot;

    [Header("VFX&SFX")]
    protected GameObject spark;
    protected AudioSource firingSound;

    private void Awake()
    {
        CreateBullets();
    }

    protected void CreateBullets()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = Instantiate(bulletPrefabs);
            tmp.SetActive(false);
            poolBullets.Add(tmp);
        }
    }
    protected GameObject GetBullet()
    {
        for(int i = 0;i < poolBullets.Count; i++)
        {
            if (!poolBullets[i].activeInHierarchy)
            {
                return poolBullets[i];
            }
        }
        return null;
    }
    protected virtual void Shoot()
    {
        timeSinceLastShot = 0f;
        //get bullet
        GameObject bullet = GetBullet();
        bullet.transform.position = shootPos.position;
        bullet.transform.rotation = shootPos.rotation;
        //play effects
        Instantiate(spark, shootPos);
        firingSound.Play();
        //shoot
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPos.forward * shootForce , ForceMode.Impulse);
    }
}
