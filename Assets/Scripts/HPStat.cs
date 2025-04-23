using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stats/HPStat")]
public class HPStat : PlayerStat
{
	public void InitFromGameManager()
	{
		if (GameManager.Instance != null)
		{
			Initialize(GameManager.Instance.playerHP);
		}
	}
}
