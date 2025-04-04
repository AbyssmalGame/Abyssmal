using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Weapon : MonoBehaviour
{
    /*  Notes ---
     *      Need 
     * 
     * 
     * 
     * 
     */
    public string name; 
    public SteamVR_Action_Single fireAction;
    public GameObject projectile;
    public GameObject barrelPivot;
    public float shootSpeed;
    
    public int damage;
    public float fireCooldownSeconds;
    public int magazine;
    public int level;

    private int currentMagazine;
    private Interactable interactable;
    private bool onCooldown;

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
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
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
            GameObject newProjectileGO = Instantiate(projectile, barrelPivot.transform.position, projectile.transform.rotation).gameObject;
            Rigidbody projectileRB = newProjectileGO.GetComponent<Rigidbody>();
            projectileRB.constraints = RigidbodyConstraints.None; // By default projectiles are inside the weapons and disabled. The Swimming with an enabled projectile causes it to fly outside the weapon.
            projectileRB.velocity = barrelPivot.transform.right * shootSpeed;
            yield return new WaitForSeconds(fireCooldownSeconds);
            onCooldown = false;
        } else
        {
            // Play gun click sound effect.
        }
        yield return null;
    }

    private void triggerMuzzleEffect()
    {
        
    }
}
