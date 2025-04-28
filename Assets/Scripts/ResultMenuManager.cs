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
	void Start()
    {
        goldText.text = "" + resultsManager.obtainedGold;
		ironText.text = "" + resultsManager.obtainedIron;
		debrisText.text = "" + resultsManager.obtainedDebris;
	}

}
