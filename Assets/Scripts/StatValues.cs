using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatValues
{
    public static List<Upgrade> PlayerHPLevels = new List<Upgrade> { new Upgrade(100f, 0, 0, 0, true), new Upgrade(120f, 10, 10, 10), new Upgrade(150f, 25, 25, 25), new Upgrade(200f, 40, 40, 40) };
    public static List<Upgrade> OxygenLevels = new List<Upgrade> { new Upgrade(600f, 0, 0, 0, true), new Upgrade(660f, 10, 10, 10), new Upgrade(720f, 25, 25, 25), new Upgrade(840f, 40, 40, 40) };
    public static List<Upgrade> SwimSpeedLevels = new List<Upgrade> { new Upgrade(4.0f, 0, 0, 0, true), new Upgrade(4.6f, 10, 10, 10), new Upgrade(5.4f, 25, 25, 25), new Upgrade(6.5f, 40, 40, 40) };

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

