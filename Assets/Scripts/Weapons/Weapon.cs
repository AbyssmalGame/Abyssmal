using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Weapon : MonoBehaviour
{
    public string name; 
    public SteamVR_Action_Single fireAction;
    public GameObject projectile;
    public GameObject barrelPivot;
    public MagazineBehavior magazineObject;
    public float shootSpeed;
    public bool ejectsMagazine = false;
    public int damage;
    public float fireCooldownSeconds;
    public int magazine;
    public int level;

    private int currentMagazine;
    private Interactable interactable;
    private bool onCooldown = false;
    private bool reloading = false;

    public void Start()
    {
        interactable = GetComponent<Interactable>();
        currentMagazine = magazine;
    }

    public void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].axis == 1)
            {
                fire();
            }
        }

    }
    public void fire()
    {
        if (!onCooldown)
        {
            StartCoroutine(startFire());
        }
    }

    public IEnumerator startFire()
    {
        if (currentMagazine != 0)
        {
            onCooldown = true;
            currentMagazine -= 1;

            GameObject newProjectileGO = Instantiate(projectile, barrelPivot.transform.position, projectile.transform.rotation).gameObject;
            Rigidbody projectileRB = newProjectileGO.GetComponent<Rigidbody>();
            projectileRB.constraints = RigidbodyConstraints.None; // Original projectiles are inside the weapons and locked in place. Swimming with an unlocked projectile causes it to fly outside the weapon.
            projectileRB.velocity = barrelPivot.transform.right * shootSpeed;
            StartCoroutine(newProjectileGO.GetComponent<Projectile>().SelfDestruct()); 

            if (magazineObject != null && ejectsMagazine && currentMagazine == 0)
            {
                magazineObject.Eject();
            }

            yield return new WaitForSeconds(fireCooldownSeconds);
            onCooldown = false;
        } else
        {
            /* Play gun click sound effect. */
        }
        yield return null;
    }

    public int getCurrentMagazine()
    {
        return currentMagazine;
    }

    public void reload()
    {
        magazineObject.Reload();
        currentMagazine = magazine;
    }

    private void triggerMuzzleEffect()
    {
        
    }
}
