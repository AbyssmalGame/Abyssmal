using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ReloadItemSpawner : MonoBehaviour
{
    private Interactable interactable;
    private GameObject reloadItem;
    private Player player;
    private void Start()
    {
        interactable = GetComponent<Interactable>();
        player = GetComponent<Player>();
    }

    public void updateReloadItem()
    {
        reloadItem = GameObject.Find("GameManager").GetComponent<WeaponManager>().currentWeapon.GetComponent<Weapon>().reloadItem.gameObject;
    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //Debug.Log("DOING SHIT LETS GO");
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            GameObject newItem = Instantiate(reloadItem, reloadItem.transform.position, reloadItem.transform.rotation, reloadItem.transform);
            newItem.transform.localScale = Vector3.one;
            newItem.transform.parent = null;
            if (newItem.activeInHierarchy == false)
            {
                newItem.SetActive(true);
            }
            hand.AttachObject(newItem, grabType);
            hand.HoverLock(newItem.GetComponent<Interactable>());
        }
    }
}
