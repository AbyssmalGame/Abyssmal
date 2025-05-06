using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatValues
{
    public static List<Upgrade> PlayerHPLevels = new List<Upgrade> { new Upgrade(100f, 0, 0, 0, true), new Upgrade(120f, 1, 1, 1), new Upgrade(150f, 6, 2, 14), new Upgrade(200f, 10, 10, 10) };
    public static List<Upgrade> OxygenLevels = new List<Upgrade> { new Upgrade(600f, 0, 0, 0, true), new Upgrade(660f, 1, 1, 1), new Upgrade(720f, 7, 5, 20), new Upgrade(840f, 10, 10, 10) };
    public static List<Upgrade> SwimSpeedLevels = new List<Upgrade> { new Upgrade(1f, 0, 0, 0, true), new Upgrade(1.35f, 1, 1, 1), new Upgrade(1.75f, 12, 42, 8), new Upgrade(3.35f, 10, 10, 10) };

    public static Upgrade SpearGun = new Upgrade(0f, 0, 0, 0, true);
    public static Upgrade HarpoonGun = new Upgrade(1f, 15, 15, 15);
    public static Upgrade APSRifle = new Upgrade(2f, 35, 35, 35);

    public static List<Upgrade> Weapons = new List<Upgrade> { SpearGun, HarpoonGun, APSRifle };

    public static List<string> WeaponNames = new List<string>{ "spearGun", "harpoonGun", "apsRifle" };

}

public class Upgrade
{
    public float levelValue;
    public bool isOwned;
    public int goldCost;
    public int ironCost;
    public int debrisCost;

    public Upgrade(float levelValue, int goldCost, int ironCost, int debrisCost, bool isOwned = false)
    {
        this.levelValue = levelValue;
        this.goldCost = goldCost;
        this.ironCost = ironCost;
        this.debrisCost = debrisCost;
        this.isOwned = isOwned;
    }

}



/*
 * Stat levels 1-4
 * Costs for level 1-4
 * 
 * 
 * 
 */

