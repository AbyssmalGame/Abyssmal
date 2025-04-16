using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Weapon : MonoBehaviour
{ 
    public SteamVR_Action_Single fireAction;
    public GameObject projectile;
    public GameObject barrelPivot;
    public MagazineBehavior magazineObject;
    public ReloadItem reloadItem;
    public float shootSpeed;
    public bool ejectsMagazine = false;
    public int damage;
    public float fireCooldownSeconds;
    public int magazine;
    public int level;

    [SerializeField] private int currentMagazine;
    [SerializeField] private bool attachedToHand;
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
        attachedToHand = interactable.attachedToHand;
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].axis == 1) //fully pulled trigger
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
            Debug.Log("FIRING");
            onCooldown = true;
            currentMagazine -= 1;

            GameObject newProjectileGO = Instantiate(projectile, barrelPivot.transform.position, projectile.transform.rotation, gameObject.transform).gameObject;
            newProjectileGO.transform.parent = null;
            Rigidbody projectileRB = newProjectileGO.GetComponent<Rigidbody>();
            projectileRB.constraints = RigidbodyConstraints.None; // Original projectiles are inside the weapons and locked in place. Swimming with an unlocked projectile causes it to fly outside the weapon.
            projectileRB.velocity = barrelPivot.transform.forward * shootSpeed;
            StartCoroutine(newProjectileGO.GetComponent<Projectile>().SelfDestruct()); 

            if (magazineObject != null && ejectsMagazine && currentMagazine == 0)
            {
                magazineObject.Eject();
            } else if (!ejectsMagazine && currentMagazine == 0)
            {
                projectile.GetComponent<MeshRenderer>().enabled = false;
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
        if (magazineObject != null)
        {

            magazineObject.Reload();
        } else
        {
            projectile.GetComponent<MeshRenderer>().enabled = true;
        }
        currentMagazine = magazine;
    }

    private void triggerMuzzleEffect()
    {
        
    }
}
