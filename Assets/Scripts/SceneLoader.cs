using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class SceneLoader : MonoBehaviour
{
    public GameObject PlayerGO;
    private Player Player;
    public GameObject gameManager;
    private WeaponManager weaponManager;

    private void Start()
    {
        Player = PlayerGO.GetComponent<Player>();
        weaponManager = gameManager.GetComponent<WeaponManager>();
    }

    public void LoadScene(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
        if (weaponManager != null)
        {
            if (sceneIndex != 0)
            {
                weaponManager.enabled = true;
                weaponManager.InitialIze();
            }
            else
            {
                weaponManager.enabled = false;
                Player.rightHand.DetachObject(weaponManager.getWeapon1());
                Player.rightHand.DetachObject(weaponManager.getWeapon2());
            }
        }

        if (PlayerGO != null) 
        {
            Destroy(PlayerGO);
        }
        
    }
}
