using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

	private void OnEnable()
	{
        RefreshButtons();
	}

    private void RefreshButtons()
    {
        if (GameManager.Instance == null) return;

        for (int i = 0; i < buttons.Length; i++)
        {
            bool unlocked = GameManager.Instance.IsLevelUnlocked(i);

			//remove comments later when done testing
            buttons[i].gameObject.SetActive(unlocked);
        }
    }

}
