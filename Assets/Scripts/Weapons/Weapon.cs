using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public abstract class Weapon : MonoBehaviour
{
    /*  Notes ---
     *      Need 
     * 
     * 
     * 
     * 
     */
    public SteamVR_Action_Boolean fireAction;
    public GameObject projectile;
    public float shootSpeed;
    
    public int damage;
    public float fireCooldownSeconds;
    public int magazine;
    public int level;


    private Interactable interactable;
    private bool onCooldown;

    public void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    public void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].stateDown)
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
            yield return new WaitForSeconds(fireCooldownSeconds);
            onCooldown = false;
        }
        yield return null;
    }

    private void triggerMuzzleEffect()
    {
        
    }
}
