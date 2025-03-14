using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;


public class MenuImageHandler : MonoBehaviour
{
    private GameManager gameManager;
    public TMPro.TextMeshProUGUI upgradeValueText;
    public string upgradeName;
    public Image upgradeImageUI;
    public List<Sprite> upgradeSprites;
    public Button equipButton;
    public TMPro.TextMeshProUGUI equipText;

    private int currentUpgradeIndex = 0;

    void Start()
    {
        UpdateUI();
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
            UpdateUI();
        }


    }

    private void UpdateUI()
    {
        switch (upgradeName)
        {
            case "Suit":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Suit upgrade granting " + StatValues.PlayerHPLevels[currentUpgradeIndex] + " HP";
                break;
            case "Oxygen":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Oxygen upgrade granting " + StatValues.OxygenLevels[currentUpgradeIndex] + " O2";
                break;
            case "SwimSpeed":
                upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
                upgradeValueText.text = "Flipper upgrade granting " + StatValues.PlayerHPLevels[currentUpgradeIndex] + "x speed";
                break;
        }
        
        
    }

    public void BuyUpgrade()
    {
        switch (upgradeName)
        {
            case "Suit":

                gameManager.SetHP((int) StatValues.PlayerHPLevels[currentUpgradeIndex].levelValue);
                break;
            case "Oxygen":
                gameManager.SetOxygen(StatValues.OxygenLevels[currentUpgradeIndex].levelValue);
                break;
            case "SwimSpeed":
                gameManager.SetSwimSpeed(StatValues.SwimSpeedLevels[currentUpgradeIndex].levelValue);
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
    
}
