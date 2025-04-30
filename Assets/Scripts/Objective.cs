using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Objective : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private Interactable interactable;
    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
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
