using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject Player;
    public ResultsManager resultsManager;
    public FadeScreen fadeScreen;
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
