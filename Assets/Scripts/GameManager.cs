using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatValues;

public class GameManager : MonoBehaviour
{
	/*
	 * Purely used to handle default stats in game scenes. Values should only be edited inside the upgrade menus
	 */

    public static GameManager Instance;

    public int playerHP = (int) StatValues.PlayerHPLevels[0].levelValue;
    public float oxygen = StatValues.OxygenLevels[0].levelValue; 
    public float swimSpeed = StatValues.SwimSpeedLevels[0].levelValue;
    public int gold = 0;
    public int iron = 0;
    public int debris = 0;
    public string weapon1 = "harpoon";
    public string weapon2 = "none";

	private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
    public void SetHP(int amount) 
    {
        playerHP = amount;
    }
	public void SetOxygen(float amount)
	{
		oxygen = amount;
	}
	public void SetSwimSpeed(float amount)
	{
		swimSpeed = amount;
	}
	public void SetWeapon1(string weapon)
	{
		weapon1 = weapon;
	}
	public void SetWeapon2(string weapon)
	{
		weapon2 = weapon;
	}

	public static float GetSpeed()
    {
		return  Instance.swimSpeed;
    }

	public void AddGold(int amount) { gold += amount; }
	public void SpendGold(int amount) { if (gold >= amount) gold -= amount; }
	public void AddIron(int amount) { iron += amount; }
	public void SpendIron(int amount) { if (iron >= amount) iron -= amount; }
	public void AddDebris(int amount) { debris += amount; }
	public void SpendDebris(int amount) { if (debris >= amount) debris -= amount; }
}
