using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponManager : MonoBehaviour
{
    public GameManager gameManager;
    public SteamVR_ActionSet actions;

    private Weapon w1;
    private Weapon w2;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player  = GameObject.Find("Player").GetComponent<Player>();
        w1 = fetchWeapon(gameManager.GetWeapon1());
        actions.Activate();
        if (gameManager.GetWeapon2() != "" )
        {
            w2 = fetchWeapon(gameManager.GetWeapon2());
        }
        Debug.Log("RIGHT HAND: " + player.rightHand);
        if (player.rightHand != null) 
        {
            StartCoroutine(lateAttach(player.rightHand, w1.gameObject));
        }
    }

    private IEnumerator lateAttach(Hand hand, GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        hand.AttachObject(obj, GrabTypes.None);
    }

    void switchWeapon()
    {
        if (w1.enabled == true)
        {
            w1.enabled = false;
            w2.enabled = true;
        } else
        {
            w2.enabled = true;
            w1.enabled = false;
        }
    }



    public Weapon fetchWeapon(string name)
    {
        Debug.Log("WEAPON1: " + gameManager.GetWeapon1());
        return GameObject.Find(name).GetComponent<Weapon>();
    }
}
