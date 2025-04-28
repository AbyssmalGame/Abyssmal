using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stats/OxygenStat")]
public class OxygenStat : PlayerStat
{
	public void InitFromGameManager()
	{
		if (GameManager.Instance != null)
		{
			Initialize(GameManager.Instance.oxygen);
		}
	}
}
