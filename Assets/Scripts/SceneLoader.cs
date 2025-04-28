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
        resultsManager.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(FadeAndLoadScene(sceneIndex));

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
    }
    public void LoadWin(int levelId)
    {
        resultsManager.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!GameManager.Instance.IsLevelUnlocked(levelId))
        {
            GameManager.Instance.UnlockLevel(levelId);
        }
        StartCoroutine(FadeAndLoadScene(6));
    }

    public void LoadLose()
    {
        resultsManager.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(FadeAndLoadScene(6));
    }

    public void RetryLevel()
    {
        StartCoroutine(FadeAndLoadScene(resultsManager.lastSceneIndex));
    }

    public void NextLevel()
    {
        int lastScene = resultsManager.lastSceneIndex;
        int nextScene = lastScene + 1;
        if (nextScene >= 5)
        {
            StartCoroutine(FadeAndLoadScene(0));
        }
        else
        {
            StartCoroutine(FadeAndLoadScene(nextScene));
        }
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
        if (Player != null)
        {
            Destroy(Player);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
