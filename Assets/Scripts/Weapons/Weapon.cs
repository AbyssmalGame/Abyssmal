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
    public ParticleSystem[] gunShotEffects;
    public float shootSpeed;
    public bool ejectsMagazine = false;
    public int damagePerShot;
    public float fireCooldownSeconds;
    public int magazine;
    public AudioSource fireSound;
    public AudioSource reloadSound;

    private int currentMagazine;
    private bool attachedToHand;
    private Interactable interactable;
    private bool onCooldown = false;
    private bool reloading = false;

    public void Start()
    {
        interactable = GetComponent<Interactable>();
        currentMagazine = magazine;
        DontDestroyOnLoad(gameObject);
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
            onCooldown = true;
            currentMagazine -= 1;

            //Instantiate new projectile in scene. Destruction coroutine is tied to the projectile itself
            projectile.SetActive(true);
            GameObject newProjectileGO = Instantiate(projectile, barrelPivot.transform.position, projectile.transform.rotation, gameObject.transform).gameObject;
            projectile.SetActive(false);
            if (newProjectileGO.TryGetComponent(out Projectile newProjectile)) {
                newProjectile.damage = damagePerShot;
                newProjectile.selfDestruct();
            }
            newProjectileGO.transform.parent = null;
            Rigidbody projectileRB = newProjectileGO.GetComponent<Rigidbody>();
            projectileRB.constraints = RigidbodyConstraints.None; // Original projectiles are inside the weapons and locked in place. Swimming with an unlocked projectile causes it to fly outside the weapon.
            fireSound.PlayOneShot(fireSound.clip);
            foreach (ParticleSystem effect in gunShotEffects)
            {
                effect.transform.parent = null;
                effect.Play();
                effect.transform.parent = transform;
            }
            projectileRB.velocity = barrelPivot.transform.forward * shootSpeed; 

            if (magazineObject != null && ejectsMagazine && currentMagazine == 0)
            {
                magazineObject.Eject();
            } else if (!ejectsMagazine && currentMagazine == 0)
            {
                projectile.SetActive(false);
            }

            yield return new WaitForSeconds(fireCooldownSeconds);
            onCooldown = false;
        } else
        {
            /* Play gun click sound effect. */
        }
        yield return null;
    }

    public void forceCooldownUpdate(bool onCooldownState)
    {
        onCooldown = onCooldownState;
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
            projectile.SetActive(true);
            Debug.Log("RELOADED HARPOON GUN");
        }
        currentMagazine = magazine;
        reloadSound.PlayOneShot(reloadSound.clip);

    }

    private void triggerMuzzleEffect()
    {
        
    }
}
