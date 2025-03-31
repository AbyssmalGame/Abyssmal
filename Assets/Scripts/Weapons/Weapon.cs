using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;  // When balancing change this in each weapon.
    public float fireCooldownSeconds;
    public GameObject projectile;
    public int magazine;
    public int level;
    public GameObject projectile;
    private bool onCooldown;

    public void Awake()
    {
        
    }
    public void fireCallback()
    {
        if (!onCooldown)
        {
            StartCoroutine(startFire());
        }
    }

    public IEnumerator startFire()
    {
        if (magazine != 0)
        {
            onCooldown = true;
            /*
             * 1. Generate projectile
             *  1a. Add force to projectile to shoot forward
             * 2. Account for damage colliders on the projectile
             * 3. Set magazine to x - 1;
             * 
             * 
             */
            magazine -= 1;
            onCooldown = false;
        }
        yield return null;
    }
}
