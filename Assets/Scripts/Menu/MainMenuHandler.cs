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
    public TMPro.TextMeshProUGUI equipText;
    public TMPro.TextMeshProUGUI goldCostText;
    public TMPro.TextMeshProUGUI ironCostText;
    public TMPro.TextMeshProUGUI debrisCostText;
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

    private void UpdateButtonUI(Upgrade upgrade, float levelValComapre)
    {
        if (upgrade.isOwned)
        {
            if (upgrade.levelValue != levelValComapre)
            {
                equipText.SetText("Equip");
                equipButton.GetComponent<Image>().color = Color.white;
                equipButton.interactable = true;
            }
            else
            {
                equipText.SetText("Equipped");
                equipButton.GetComponent<Image>().color = Color.gray;
                equipButton.interactable = false;
            }
        }
        else
        {
            equipText.SetText("Purchase");
            if (CheckUpgradeCost(upgrade)) 
            {
                equipButton.GetComponent<Image>().color = Color.green;
                equipButton.interactable = true;
            } else
            {
                equipButton.GetComponent<Image>().color = Color.red;
                equipButton.interactable = false;
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
