using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class SceneLoader : MonoBehaviour
{
    public GameObject PlayerGO;
    private Player Player;
    public ResultsManager resultsManager;
    public FadeScreen fadeScreen;
    private GameObject gameManager;
    private WeaponManager weaponManager;

    private void Awake()
    {
        Player = PlayerGO.GetComponent<Player>();
        gameManager = GameObject.Find("GameManager");
        weaponManager = gameManager.GetComponent<WeaponManager>();
    }

    public void LoadScene(int sceneIndex)
    {
        resultsManager.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(FadeAndLoadScene(sceneIndex));
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
        resultsManager.loseLevel = true;
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (weaponManager != null)
        {
            Debug.Log("Weapon Manager not null");
            if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 6)
            {
                weaponManager.enabled = true;
                weaponManager.InitialIze();
            }
            else
            {
                weaponManager.enabled = false;

            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void DetachAllObjectsFromPlayer()
    {
        if (Player == null) return;

        if (Player.leftHand != null)
        {
            DetachAllFromHand(Player.leftHand);
        }

        if (Player.rightHand != null)
        {
            DetachAllFromHand(Player.rightHand);
        }
    }

    private void DetachAllFromHand(Hand hand)
    {
        // Make a copy of the attached objects list first
        var attachedObjectsCopy = new List<Hand.AttachedObject>(hand.AttachedObjects);

        foreach (var attached in attachedObjectsCopy)
        {
            if (attached.attachedObject != null)
            {
                hand.DetachObject(attached.attachedObject);
            }
        }
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Player != null)
        {
            DetachAllObjectsFromPlayer();
            Destroy(PlayerGO);
        }
        yield return null;
        SceneManager.LoadScene(sceneIndex);
    }
}
