using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{
	public HPStat hpStat;
	public OxygenStat oxygenStat;

	public TextMeshProUGUI hpText;
	public TextMeshProUGUI oxygenText;
	private int lastDisplayedOxygen = -1;

	public Image hpIcon;
	public Sprite greenSprite;
	public Sprite yellowSprite;
	public Sprite redSprite;

	public SceneLoader sceneLoader;

	void Start()
	{
		UpdateMenuStats();
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Results") return;

		if (oxygenStat.currentValue > 0)
		{
			oxygenStat.currentValue -= Time.deltaTime;

			int oxygenInt = Mathf.FloorToInt(oxygenStat.currentValue);

			if (oxygenInt % 10 == 0 && oxygenInt != lastDisplayedOxygen)
			{
				lastDisplayedOxygen = oxygenInt;
				oxygenText.text = "" + oxygenInt;
			}
		}
		else if (oxygenStat.currentValue < 0)
		{
			sceneLoader.LoadScene(0);
		}
	}

	public void UpdateMenuStats()
	{
		hpStat.InitFromGameManager();
		oxygenStat.InitFromGameManager();

		hpText.text = "" + hpStat.currentValue;
		oxygenText.text = "" + oxygenStat.currentValue;
		UpdateHPIcon();
	}

	public void TakeDamage(float amount)
	{
		hpStat.currentValue -= amount;
		hpText.text = "" + Mathf.FloorToInt(hpStat.currentValue);

		UpdateHPIcon();

		if (hpStat.currentValue <= 0)
		{
			sceneLoader.LoadScene(0);
		}
	}

	void UpdateHPIcon()
	{
		float hpPercent = hpStat.currentValue / hpStat.maxValue;

		if (hpPercent > 0.5f)
		{
			hpIcon.sprite = greenSprite;
		}
		else if (hpPercent > 0.2f)
		{
			hpIcon.sprite = yellowSprite;
		}
		else
		{
			hpIcon.sprite = redSprite;
		}
	}
}
