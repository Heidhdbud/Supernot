using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("BulletPooling")]
    protected List<GameObject> poolBullets = new List<GameObject>();
    [SerializeField] protected int amountToPool;
    [SerializeField] protected GameObject bulletPrefabs;

    [Header("Shooting")]
    [SerializeField] protected Transform shootPos;
    [SerializeField] protected float shootForce;
    [SerializeField] protected float shootDelay;
    protected float timeSinceLastShot = 0f;

    [Header("VFX&SFX")]
    [SerializeField,Tooltip("For player use the gun barrel end position. For enemy use the same as shoot position")] Transform sparkPos;
    [SerializeField] protected GameObject spark;
    [SerializeField] protected AudioSource firingSound;

    protected void Awake()
    {
        CreateBullets();
    }
    protected virtual void Update()
    {
        timeSinceLastShot += Time.deltaTime;
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
    protected void Shoot()
    {
        timeSinceLastShot = 0f;
        //get bullet
        GameObject bullet = GetBullet();
        bullet.SetActive(true);
        bullet.transform.position = shootPos.position;
        bullet.transform.rotation = shootPos.rotation;
        //play effects
        GameObject sparks = Instantiate(spark,sparkPos.position,sparkPos.rotation);
        sparks.transform.parent = sparkPos;
        firingSound.Play();
        //shoot
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPos.forward * shootForce);
    }
}
