using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;
using TMPro;


public class MainMenuHandler : MonoBehaviour
{
    private GameManager gameManager;
    public TMPro.TextMeshProUGUI upgradeValueText;
    public string upgradeName;
    public Image upgradeImageUI;
    public List<Sprite> upgradeSprites;
    public Button equipButton;
    public Button equipButton2;
    public TMPro.TextMeshProUGUI equipText;
    public TMPro.TextMeshProUGUI goldCostText;
    public TMPro.TextMeshProUGUI ironCostText;
    public TMPro.TextMeshProUGUI debrisCostText;
    public List<Image> weaponImageUI;
    public TMPro.TextMeshProUGUI weaponTextUI;
    private CurrencyUIHandler currencyUIHandler;

    public PlayerStatManager playerStatManager;

    private int currentUpgradeIndex = 0;

    void Start()
    {
        UpdateUI();
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
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        switch (upgradeName)
        {
            case "Suit":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Suit upgrade granting " + StatValues.PlayerHPLevels[currentUpgradeIndex].levelValue + " HP";
                UpdateButtonUI(StatValues.PlayerHPLevels[currentUpgradeIndex], gameManager.playerHP);
                UpdateCostUI(StatValues.PlayerHPLevels[currentUpgradeIndex]);
                break;

            case "Oxygen":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Oxygen upgrade granting " + StatValues.OxygenLevels[currentUpgradeIndex].levelValue + " O2";
                UpdateButtonUI(StatValues.OxygenLevels[currentUpgradeIndex], gameManager.oxygen);
                UpdateCostUI(StatValues.OxygenLevels[currentUpgradeIndex]);
                break;

            case "SwimSpeed":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Flipper upgrade granting " + StatValues.SwimSpeedLevels[currentUpgradeIndex].levelValue + "x speed";
                UpdateButtonUI(StatValues.SwimSpeedLevels[currentUpgradeIndex], gameManager.swimSpeed);
                UpdateCostUI(StatValues.SwimSpeedLevels[currentUpgradeIndex]);
                break;
        }
        currencyUIHandler.updateCurrencyUI();

        playerStatManager.UpdateMenuStats();
    }

    private void UpdateWeaponUI(int uiIndex = 0)
    {
        weaponImageUI[uiIndex].sprite = upgradeSprites[currentUpgradeIndex];
        switch (currentUpgradeIndex)
        {
            
                
        }
        if (uiIndex == 0)
        {
            
        } else if (uiIndex == 1)
        {
            
        }
        upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
        upgradeValueText.text = "Single shot speargun";
        if (uiIndex== 0)
        {

        }
        //UpdateButtonUI(StatValues.SpearGun, equipButton);
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
