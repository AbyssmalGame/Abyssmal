using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;
using TMPro;
using UnityEngine.AI;


public class WeaponMenuHandler: MonoBehaviour
{
    private GameManager gameManager;
    public Image upgradeImageUI;
    public List<Sprite> upgradeSprites;
    public List<Button> equipButtons;
    public List<TextMeshProUGUI> equipTexts;
    public TextMeshProUGUI goldCostText;
    public TextMeshProUGUI ironCostText;
    public TextMeshProUGUI debrisCostText;
    public List<Image> weaponImageUIs;
    public TextMeshProUGUI weaponDescriptionText;
    public List<TextMeshProUGUI> weaponNameTexts;
    public CurrencyUIHandler currencyUIHandler;

    private int currentUpgradeIndex = 0;
    private string[] weaponDescriptions = { "A single shot speargun.", "A single shot harpoon gun", "A rapid fire APS rifle." };

    void Start()
    {
        UpdateWeaponUI(0);
        UpdateEquippedWeaponUI();
    }
        
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SwitchUpgrade(int direction)
    {
        if ((currentUpgradeIndex < upgradeSprites.Count - 1 && direction == 1) || (direction == -1 && currentUpgradeIndex > 0)) 
        {
            currentUpgradeIndex += direction;
            UpdateWeaponUI(0);
        }
    }

    // uiIndex represents weapon1 or weapon2 on the menu
    private void UpdateWeaponUI(int uiIndex = -1)
    {
        upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
        weaponDescriptionText.text = weaponDescriptions[currentUpgradeIndex];
        UpdateButtonUI(Weapons[currentUpgradeIndex]);
        UpdateCostUI(Weapons[currentUpgradeIndex]);
        UpdateButtonUI(Weapons[currentUpgradeIndex]);
        if (uiIndex > -1)
        {
            UpdateEquippedWeaponUI();
        }
    }

    private int getWeaponIndexByName(string weaponName)
    {
        int weaponIndex = -1;
        if (weaponName == WeaponNames[0]) //speargun
        {
            weaponIndex = 0;
        } 
        else if (weaponName == WeaponNames[1]) //harpoonGun
        {
            weaponIndex = 1;
        } 
        else if (weaponName != WeaponNames[2]) //APSrifle
        {
            weaponIndex = 2;
        }
        return weaponIndex; 
    }

    /// <summary>
    /// Updates weapon UI representing currently equipped weapons. If you're calling this, it should only be called after updating the gameManager 
    /// </summary>
    private void UpdateEquippedWeaponUI()
    {
        for (int i = 0; i < equipButtons.Count; i++)
        {
            string equippedWeaponName = gameManager.GetWeaponAtIndex(i); // i should only ever be 0 or 1
            if (equippedWeaponName != "")
            {
                weaponNameTexts[i].text = GetFormattedWeaponName(equippedWeaponName);
                weaponImageUIs[i].sprite = upgradeSprites[WeaponNames.IndexOf(equippedWeaponName)];
            }

        }

    }

    private string GetFormattedWeaponName(string weaponName)
    {
        if (weaponName == WeaponNames[0])
        {
            return "Spear Gun";
        } else if (weaponName == WeaponNames[1])
        {
            return "Harpoon Gun";
        } else if (weaponName == WeaponNames[2])
        {
            return "APS Rifle";
        }
        return null;
    }

    private void UpdateWeaponImage(int uiIndex = 0)
    {
        weaponImageUIs[uiIndex].sprite = upgradeSprites[currentUpgradeIndex];
    }

    private void UpdateButtonUI(Upgrade upgrade)
    {
        for (int i = 0; i < equipButtons.Count; i++)
        {
            if (upgrade.isOwned)
            {
                if (gameManager.GetWeaponAtIndex(i) != WeaponNames[currentUpgradeIndex])
                {
                    equipTexts[i].SetText("Equip");
                    equipButtons[i].GetComponent<Image>().color = Color.white;
                }
                else
                {
                    equipTexts[i].SetText("Equipped");
                    equipButtons[i].GetComponent<Image>().color = Color.gray;
                }
            }
            else
            {
                equipTexts[i].SetText("Purchase");
                if (CheckUpgradeCost(upgrade))
                {
                    equipButtons[i].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    equipButtons[i].GetComponent<Image>().color = Color.red;
                }
            }
        }
        
    }

    /*
     * TODO:
     * When a weapon is bought:
     * - Update equipped image UI at uiIndex (0 or 1) 
     * - Update equipped text at uiIndex
     * - Set Game Manager weapon based on uiIndex as appropriate weapon text
     * 
     */
    public void BuyWeapon(int uiIndex)
    {
        Upgrade current = Weapons[currentUpgradeIndex];
        string weaponToBuy = "";
        // Get weapon name by index
        if (current.isOwned || CheckUpgradeCost(current))
        {
            UpdateGameManagerCosts(current);
            weaponToBuy = WeaponNames[currentUpgradeIndex];
        }
        
        //Set gamemanager
        if (weaponToBuy != "")
        {
            if (uiIndex == 0)
            {
                gameManager.SetWeapon1(weaponToBuy);
                    
            }
            else if (uiIndex == 1)
            {
                gameManager.SetWeapon2(weaponToBuy);
            }
            UpdateWeaponUI(uiIndex);
            UpdateEquippedWeaponUI();
        }
        
    }

    private bool CheckUpgradeCost(Upgrade price)
    {
        if ((gameManager.gold >= price.goldCost) && (gameManager.iron >= price.ironCost) && (gameManager.debris >= price.debrisCost))
        {
            return true;
        }
        return false;
    }

    private void UpdateGameManagerCosts(Upgrade price)
    {
        gameManager.gold -= price.goldCost;
        gameManager.iron -= price.ironCost;
        gameManager.debris -= price.debrisCost;
        price.isOwned = true;
    }

    private void UpdateCostUI(Upgrade price)
    {
        goldCostText.text = price.goldCost.ToString();
        ironCostText.text = price.ironCost.ToString();
        debrisCostText.text = price.debrisCost.ToString();
    }
    
}
