using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;
using TMPro;


public class WeaponMenuHandler: MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI upgradeValueText;
    public string upgradeName;
    public Image upgradeImageUI;
    public List<Sprite> upgradeSprites;
    public Button equipButton;
    public Button equipButton2;
    public List<TextMeshProUGUI> equipTexts;
    public TextMeshProUGUI goldCostText;
    public TextMeshProUGUI ironCostText;
    public TextMeshProUGUI debrisCostText;
    public List<Image> weaponImageUI;
    public TextMeshProUGUI weaponDescriptionText;
    public List<TextMeshProUGUI> weaponNameTexts;
    private CurrencyUIHandler currencyUIHandler;

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
        currencyUIHandler = GameObject.Find("Currency").GetComponent<CurrencyUIHandler>();
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
        weaponNameTexts[currentUpgradeIndex].text = StatValues.WeaponList[currentUpgradeIndex];
        upgradeValueText.text = weaponDescriptions[currentUpgradeIndex];
        UpdateButtonUI()
        UpdateCostUI(StatValues.)
        //UpdateButtonUI(StatValues.SpearGun, equipButton);
    }

    private void UpdateWeaponImage(int uiIndex = 0)
    {
        weaponImageUI[uiIndex].sprite = upgradeSprites[currentUpgradeIndex];
    }

    private void UpdateButtonUI(Upgrade upgrade, float levelValComapre)
    {
        if (upgrade.isOwned)
        {
            if (upgrade.levelValue != levelValComapre)
            {
                equipText.SetText("Equip");
                equipButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                equipText.SetText("Equipped");
                equipButton.GetComponent<Image>().color = Color.gray;
            }
        }
        else
        {
            equipText.SetText("Purchase");
            if (CheckUpgradeCost(upgrade)) 
            {
                equipButton.GetComponent<Image>().color = Color.green;
            } else
            {
                equipButton.GetComponent<Image>().color = Color.red;
            }
        }
    }

    private void UpdateButtonUI(Upgrade weapon, TMPro.TextMeshProUGUI text, Button button, string currentWeapon, string compareWeapon)
    {
        if (weapon.isOwned)
        {
            if (currentWeapon != compareWeapon)
            {
                text.SetText("Equip");
                button.GetComponent<Image>().color = Color.white;
            }
            else
            {
                text.SetText("Equipped");
                button.GetComponent<Image>().color = Color.gray;
            }
        } else
        {
            equipText.SetText("Purchase");
            if (CheckUpgradeCost(weapon))
            {
                button.GetComponent<Image>().color = Color.green;
            }
            else
            {
                button.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void BuyWeapon(int slotIndex)
    {
        string weaponToBuy = "";
        if (upgradeName == "Weapon")
        {
            switch (currentUpgradeIndex)
            {
                case 0: //Speargun
                    if (StatValues.SpearGun.isOwned || CheckUpgradeCost(StatValues.SpearGun))
                    {
                        UpdateGameManagerCosts(StatValues.SpearGun);
                        weaponToBuy = "spearGun";
                    }
                    break;

                case 1: //HarpoonGun
                    if (StatValues.HarpoonGun.isOwned || CheckUpgradeCost(StatValues.HarpoonGun))
                    {
                        UpdateGameManagerCosts(StatValues.HarpoonGun);
                        weaponToBuy = "harpoonGun";
                    }
                    break;

                case 2: //APS Rifle
                    if (StatValues.APSRifle.isOwned || CheckUpgradeCost(StatValues.APSRifle))
                    {
                        UpdateGameManagerCosts(StatValues.APSRifle);
                        weaponToBuy = "apsRifle";
                    }
                    break;
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
            }
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
