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
    private bool fading = false;

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
        if (lastScene >= 5)
        {
			StartCoroutine(FadeAndLoadScene(0));
		}
        else
        {
            StartCoroutine(FadeAndLoadScene(nextScene));
        }
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(waitForWeaponManagerLoad(SceneManager.GetActiveScene().buildIndex));
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator waitForWeaponManagerLoad(int sceneIndex)
    {
        Debug.Log("Waiting for weapon manager on scene: " + sceneIndex);
        GameObject gameManager = null;
        WeaponManager weaponManager = null;

        while ((gameManager = GameObject.Find("GameManager")) == null)
            yield return null;
        Debug.Log("Found GameManager...");
        while ((weaponManager = gameManager.GetComponent<WeaponManager>()) == null)
            yield return null;
        Debug.Log("Found WeaponManager...");
        if (sceneIndex != 0 && sceneIndex != 6)
        {
            weaponManager.enabled = true;
            Debug.Log("Initializing");
            yield return weaponManager.InitialIze();
        }
        else
        {
            weaponManager.enabled = false;
            Debug.Log("Didn't Initialize...");
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("Destroying SceneLoader");
        Destroy(gameObject);
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        if (!fading)
        {
            fading = true;
            fadeScreen.FadeOut();

            // Launch the new scene
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;
            float timer = 0;
            while (timer <= fadeScreen.fadeDuration && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            if (Player != null)
            {
                DetachAllObjectsFromPlayer();
                Destroy(PlayerGO);
            }
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            operation.allowSceneActivation = true;
            fading = false;
        }
    }
}
