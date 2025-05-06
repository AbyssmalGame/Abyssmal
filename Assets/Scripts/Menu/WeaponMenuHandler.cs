using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;
using TMPro;
using UnityEngine.AI;


public class WeaponMenuHandler: MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI upgradeValueText;
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

    public PlayerStatManager playerStatManager;

    private int currentUpgradeIndex = 0;
    private string[] weaponDescriptions = { "A single shot speargun.", "A single shot harpoon gun", "A rapid fire APS rifle." };

    void Start()
    {
        UpdateWeaponUI(0);
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
    private void UpdateWeaponUI(int uiIndex = 0)
    {
        weaponNameTexts[currentUpgradeIndex].text = WeaponNames[currentUpgradeIndex];
        upgradeValueText.text = weaponDescriptions[currentUpgradeIndex];
        UpdateButtonUI(Weapons[currentUpgradeIndex]);
        UpdateCostUI(Weapons[currentUpgradeIndex]);
        UpdateButtonUI(Weapons[currentUpgradeIndex]);
    }

    private void UpdateEquippedWeaponUI(string weaponName, int uiIndex)
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

        
    }
    /// <summary>
    /// This versions hould only be used in an active environment. Use the string parameter on start.
    /// </summary>
    private void UpdateEquippedWeaponUI()
    {

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
                if (weaponImageUIs[i] != upgradeImageUI)
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

    public void BuyWeapon(int slotIndex)
    {
        Upgrade current = Weapons[currentUpgradeIndex];
        string weaponToBuy = "";
        if (current.isOwned || CheckUpgradeCost(current))
        {
            UpdateGameManagerCosts(current);
            switch (currentUpgradeIndex)
            {
                case 0:
                    weaponToBuy = "spearGun";
                    break;

                case 1:
                    weaponToBuy = "harpoonGun";
                    break;

                case 2:
                    weaponToBuy = "apsRifle";
                    break;
            }
        }
        
        
        if (weaponToBuy != "")
        {
            if (slotIndex == 1)
            {
                gameManager.SetWeapon1(weaponToBuy);
                    
            }
            else if (slotIndex == 2)
            {
                gameManager.SetWeapon2(weaponToBuy);
            }
            UpdateWeaponUI(slotIndex);
        }
        
    }

    public void BuyUpgrade()
    {
        switch (upgradeName)
        {
            case "Suit":
                if (StatValues.PlayerHPLevels[currentUpgradeIndex].isOwned || CheckUpgradeCost(StatValues.PlayerHPLevels[currentUpgradeIndex]))
                {
                    if (!StatValues.PlayerHPLevels[currentUpgradeIndex].isOwned)
                    {
                        UpdateGameManagerCosts(StatValues.PlayerHPLevels[currentUpgradeIndex]);
                    }
                    gameManager.SetHP((int)StatValues.PlayerHPLevels[currentUpgradeIndex].levelValue);
                    UpdateUI();
                }
                break;
            case "Oxygen":
                if (StatValues.OxygenLevels[currentUpgradeIndex].isOwned || CheckUpgradeCost(StatValues.OxygenLevels[currentUpgradeIndex]))
                {
                    if (!StatValues.OxygenLevels[currentUpgradeIndex].isOwned)
                    {
                        UpdateGameManagerCosts(StatValues.OxygenLevels[currentUpgradeIndex]);
                    }
                    gameManager.SetOxygen(StatValues.OxygenLevels[currentUpgradeIndex].levelValue);
                    UpdateUI();
                }
                
                break;
            case "SwimSpeed":
                if (StatValues.SwimSpeedLevels[currentUpgradeIndex].isOwned || CheckUpgradeCost(StatValues.SwimSpeedLevels[currentUpgradeIndex]))
                {
                    if (!StatValues.SwimSpeedLevels[currentUpgradeIndex].isOwned)
                    {
                        UpdateGameManagerCosts(StatValues.SwimSpeedLevels[currentUpgradeIndex]);
                    }
                    gameManager.SetSwimSpeed(StatValues.SwimSpeedLevels[currentUpgradeIndex].levelValue);
                    UpdateUI();
                }
                break;
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
