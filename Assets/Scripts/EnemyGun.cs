using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    [Header("Enemy")]
    [SerializeField, Range(1f, 1.75f)] private new float shootDelay;
    public bool readyToAttack;
    
    protected override void Update()
    {
        base.Update();
        if (readyToAttack)
        {
            ShootPlayer();
        }
    }
    private void ShootPlayer()
    {
        if (timeSinceLastShot >= shootDelay) 
        {
            shootDelay = Random.Range(1f, 1.75f);
            Shoot();
        }
    }
}
