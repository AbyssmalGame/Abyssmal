using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Unity.VisualScripting.Member;

public class WeaponManager : MonoBehaviour
{
    public SteamVR_Action_Boolean switchAction;
    public GameManager gameManager;
    public SteamVR_ActionSet actions;
    public int switchWeaponCooldownSeconds = 3;

    private Hand currentHand;
    private bool switching = false;
    public GameObject currentWeapon;
    private GameObject w1;
    private GameObject w2;
    private Player player;
    
    void Start()
    {
        player  = GameObject.Find("Player").GetComponent<Player>();
        w1 = fetchWeapon(gameManager.GetWeapon1());
        if (gameManager.GetWeapon2() != "" )
        {
            w2 = fetchWeapon(gameManager.GetWeapon2());
            w2.gameObject.SetActive(false);
        }

        if (player.rightHand.isPoseValid) 
        {
            attachWeapon(player.rightHand, w1);
            currentWeapon = w1;
            //switchAction.AddOnChangeListener(onSwitchAction, w1.GetComponent<Interactable>().attachedToHand.handType);
        } else
        {
            StartCoroutine(waitForValidPose());       
        }
    }

    private void Update()
    {
        if (switchAction[SteamVR_Input_Sources.RightHand].stateDown) //fully pulled trigger
        {
            switchWeapon();
        }
    }

    private IEnumerator waitForValidPose()
    {
        while (!player.rightHand.isPoseValid)
        {
            yield return new WaitForSeconds(1);
        }
        attachWeapon(player.rightHand, w1);
        currentWeapon = w1;
        GameObject.Find("ReloadItemSpawner").GetComponent<ReloadItemSpawner>().updateReloadItem();
        //switchAction.AddOnChangeListener(onSwitchAction, w1.GetComponent<Interactable>().attachedToHand.handType);
    }

    private void attachWeapon(Hand hand, GameObject obj)
    {
        hand.AttachObject(obj, GrabTypes.None);
    }

    private void onSwitchAction(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            startSwitchWeapon();
        }
    }

    public void switchWeapon()
    {
        StartCoroutine(startSwitchWeapon());
    }

    public IEnumerator startSwitchWeapon()
    {
        if (!switching)
        {
            switching = true;
            if (w1.activeSelf)
            {
                w1.SetActive(false);
                player.rightHand.DetachObject(w1);
                player.rightHand.AttachObject(w2, GrabTypes.None);
                currentWeapon = w2;
                w2.GetComponent<Interactable>().attachedToHand = player.rightHand;
                w2.SetActive(true);
                
            } else
            {
                w2.SetActive(false);
                player.rightHand.DetachObject(w2);
                player.rightHand.AttachObject(w1, GrabTypes.None);
                currentWeapon = w1;
                w1.GetComponent<Interactable>().attachedToHand = player.rightHand;
                w1.SetActive(true);
            }
            yield return new WaitForSeconds(switchWeaponCooldownSeconds);
            GameObject.Find("ReloadItemSpawner").GetComponent<ReloadItemSpawner>().updateReloadItem();
            switching = false;
        }
    }



    public GameObject fetchWeapon(string name)
    {
        return GameObject.Find(name);
    }
}
