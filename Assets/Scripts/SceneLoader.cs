using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject Player;
    public ResultsManager resultsManager;
    public void LoadScene(int sceneIndex) 
    {
        resultsManager.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        if (Player != null) 
        {
            Destroy(Player);
        }
        
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(resultsManager.lastSceneIndex);
        if (Player != null)
        {
            Destroy(Player);
        }
    }

    public void NextLevel()
    {
        int lastScene = resultsManager.lastSceneIndex;
        if (lastScene == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (lastScene == 2)
        {
            SceneManager.LoadScene(3);
        }
        else if (lastScene == 3)
        {
            SceneManager.LoadScene(4);
        }
        else if (lastScene == 4)
        {
            SceneManager.LoadScene(5);
        }
        else if (lastScene == 5)
        {
            SceneManager.LoadScene(0);
        }
        else if (lastScene == 6)
        {
            SceneManager.LoadScene(0);
        }
        if (Player != null)
        {
            Destroy(Player);
        }
    }
}
