using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun
{
    [Header("Ammo")]
    [SerializeField] int ammo;
    protected override void Update()
    {
        base.Update();
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
