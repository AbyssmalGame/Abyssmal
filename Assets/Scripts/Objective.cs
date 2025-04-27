using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    public SceneLoader sceneLoader;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int sceneId = SceneManager.GetActiveScene().buildIndex;
            if (sceneId == 5)
            {
                sceneLoader.LoadScene(6);
            }
            else
            {
                sceneLoader.LoadWin(sceneId);
            }
        }   
    }
}
