using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject Player;
    public void LoadScene(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
        if (Player != null) 
        {
            Destroy(Player);
        }
        
    }
}
