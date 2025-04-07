using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ReloadSocket : MonoBehaviour
{
    private Weapon weapon;
    private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
        weapon = GetComponentInParent<Weapon>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Debug.Log("Colliding with: " + other);
        if (other.tag == "ReloadItem" && weapon.getCurrentMagazine() < weapon.magazine)
        {
            if (weapon.magazine != 0 && weapon.ejectsMagazine)
            {
                weapon.GetComponentInChildren<MagazineBehavior>().Eject();
            }
            Interactable otherInteractable = other.GetComponent<Interactable>();
            if (otherInteractable != null && otherInteractable.attachedToHand)
            {
                otherInteractable.attachedToHand.DetachObject(other);
            }
            
            Destroy(other);
            weapon.reload();
        }
    }
}
