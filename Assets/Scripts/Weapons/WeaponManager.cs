using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Unity.VisualScripting.Member;

using UnityEngine.SceneManagement;

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
    private GameObject playerGO;
    private Player player;
    
    public IEnumerator InitialIze()
    {
        Debug.Log("Waiting for weaponManager requirements...");
        yield return StartCoroutine(waitForAllObjects());
        Debug.Log("Starting initialize weapons... SCENE: " + SceneManager.GetActiveScene().buildIndex);
        player = playerGO.GetComponent<Player>();
        
        if (gameManager.GetWeapon2() != "" )
        { 
            w2.gameObject.SetActive(false);
        }

        if (player.rightHand.isPoseValid) 
        {
            attachWeapon(player.rightHand, w1);
            currentWeapon = w1;
            GameObject.Find("Reloadspawner").GetComponent<ReloadItemSpawner>().updateReloadItem();
            Debug.Log("Attached w1 to hand");
        } else
        {
            yield return StartCoroutine(waitForValidPose());       
        }
    }
    private IEnumerator waitForAllObjects()
    {
        Debug.Log("Waiting for player");
        while ((playerGO = GameObject.Find("GamePlayer")) == null)
            yield return null;
        Debug.Log("Found player");
        if (w1 == null)
        {
            Debug.Log("Waiting for Weapon1: " + gameManager.GetWeapon1());
            while ((w1 = fetchWeapon(gameManager.GetWeapon1())) == null)
                yield return null;
            Debug.Log("Found Weapon1");
        }

        if (gameManager.GetWeapon2() != "" && w2 == null)
        {
            Debug.Log("Waiting for Weapon2: " + gameManager.GetWeapon2());
            while ((w2 = fetchWeapon(gameManager.GetWeapon2())) == null)
                yield return null;
            Debug.Log("Found Weapon2");
        }
        Debug.Log("Found all requirements");
    }

    private void Update()
    {
        if (switchAction[SteamVR_Input_Sources.RightHand].stateDown) 
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
        GameObject.Find("Reloadspawner").GetComponent<ReloadItemSpawner>().updateReloadItem();
        //switchAction.AddOnChangeListener(onSwitchAction, w1.GetComponent<Interactable>().attachedToHand.handType);
    }

    private void attachWeapon(Hand hand, GameObject obj)
    {
        hand.AttachObject(obj, GrabTypes.None);
    }

    public void switchWeapon()
    {
        StartCoroutine(startSwitchWeapon());
    }

    public IEnumerator startSwitchWeapon()
    {
        if (!switching && w2 != null)
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
    
    public GameObject getWeapon1()
    {
        return w1;
    }

    public GameObject getWeapon2()
    {
        return w2;
    }
}
