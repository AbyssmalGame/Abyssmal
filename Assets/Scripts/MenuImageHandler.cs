using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static StatValues;


public class MenuImageHandler : MonoBehaviour
{
    public GameManager gameManager;
    public TMPro.TextMeshProUGUI upgradeValueText;
    public string upgradeName;
    public Image upgradeImageUI;
    public List<Sprite> upgradeSprites;

    private int currentUpgradeIndex = 0;

    void Start()
    {
        UpdateUI();
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
                gameManager.SetHP(StatValues.PlayerHPLevels[currentUpgradeIndex]);
                break;
            case "Oxygen":
                gameManager.SetOxygen(StatValues.OxygenLevels[currentUpgradeIndex]);
                break;
            case "SwimSpeed":
                gameManager.SetSwimSpeed(StatValues.SwimSpeedLevels[currentUpgradeIndex]);
                break;
        }

    }
}
