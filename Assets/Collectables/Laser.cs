using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : Collectable
{
    

    public static  List<Projectile> projectiles = new List<Projectile>();

    public override void removeEffect()
    {
        PlatformScript.Instance.shootingDurationLeft = 0;
        PlatformScript.Instance.platformIsShooting = false;

        foreach (var projectile in projectiles.ToList())
        {
            Destroy(projectile.gameObject);
        }

        projectiles.Clear();
    }

    protected override void ApplyEffect()
    {
        PlatformScript.Instance.StartShooting();
    }
}