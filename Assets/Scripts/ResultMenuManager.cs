using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultMenuManager : MonoBehaviour
{
    public ResultsManager resultsManager;
	public TextMeshProUGUI goldText;
	public TextMeshProUGUI ironText;
	public TextMeshProUGUI debrisText;

	public GameObject[] loreMenu;
	public GameObject loseMenu;

	void Start()
    {
        goldText.text = "" + resultsManager.obtainedGold;
		ironText.text = "" + resultsManager.obtainedIron;
		debrisText.text = "" + resultsManager.obtainedDebris;

		if (resultsManager.loseLevel == true)
		{
			loseMenu.SetActive(true);
		}
		else
		{
			loreMenu[resultsManager.lastSceneIndex-1].SetActive(true);
		}
	}

}
