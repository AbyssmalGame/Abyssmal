using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHP = 100;
    public float oxygen = 120f;
    public float swimSpeed = 2;
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
	public void SetOxygen(int amount)
	{
		oxygen = amount;
	}
	public void SetSwimSpeed(int amount)
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

	public void AddGold(int amount) { gold += amount; }
	public void SpendGold(int amount) { if (gold >= amount) gold -= amount; }
	public void AddIron(int amount) { iron += amount; }
	public void SpendIron(int amount) { if (iron >= amount) iron -= amount; }
	public void AddDebris(int amount) { debris += amount; }
	public void SpendDebris(int amount) { if (debris >= amount) debris -= amount; }
}
