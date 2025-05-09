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

	[SerializeField] private AudioClip[] playerDamageSounds;
	[SerializeField] private AudioClip playerLowHPSound;
	private AudioSource audioSource;

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
			sceneLoader.LoadLose();
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
		hpStat.currentValue = hpStat.currentValue >= 0 ? hpStat.currentValue : 0;

		hpText.text = "" + Mathf.FloorToInt(hpStat.currentValue);

		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(playerDamageSounds[Random.Range(0, playerDamageSounds.Length)]);

		UpdateHPIcon();

		if (hpStat.currentValue <= 0)
		{
			sceneLoader.LoadLose();
		}
	}

	void UpdateHPIcon()
	{
		float hpPercent = hpStat.currentValue / hpStat.maxValue;

		if (hpPercent > 0.2f)
        {
			audioSource.loop = false;
		}

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
			audioSource.loop = true;
			audioSource.clip = playerLowHPSound;

			hpIcon.sprite = redSprite;
		}
	}
}
