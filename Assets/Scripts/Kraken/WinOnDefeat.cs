using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOnDefeat : MonoBehaviour
{

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject target;

    private void Update()
    {
        if (target == null)
        {
            StartCoroutine(WaitThenWin());
        }
    }

    IEnumerator WaitThenWin()
    {
        yield return new WaitForSeconds(5f);
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
