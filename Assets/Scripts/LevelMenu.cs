using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        // Loop through buttons and set interactable based on unlocked levels
        for (int i = 0; i < buttons.Length; i++)
        {
            if (GameManager.Instance.IsLevelUnlocked(i))
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}
