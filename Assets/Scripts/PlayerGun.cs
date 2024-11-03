using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    [Header("Ammo")]
    [SerializeField] int ammo;
    private void FixedUpdate()
    {
        Trigger();
    }
    private void Trigger()
    {
        if (Input.GetMouseButtonDown(0) && timeSinceLastShot >= shootDelay && ammo > 0)
        {
            ammo--;
            Shoot();
        }
    }
}
