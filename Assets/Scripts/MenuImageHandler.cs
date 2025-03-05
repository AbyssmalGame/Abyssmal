using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class MenuImageHandler : MonoBehaviour
{
    public GameManager gameManager;
    public TMPro.TextMeshProUGUI upgradeValueText;
    public string upgradeName;
    public Image upgradeImageUI;    
    public List<Sprite> upgradeSprites;
    public List<int> upgradeValues;
    

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
        upgradeImageUI.sprite = upgradeSprites[currentUpgradeIndex];
        upgradeValueText.text = upgradeName + upgradeValues[currentUpgradeIndex];
    }

    public void BuyUpgrade()
    {
        switch (upgradeName)
        {
            case "Suit":
                gameManager.SetHP(upgradeValues[currentUpgradeIndex]);
                break;
            case "O2":
                gameManager.SetOxygen(upgradeValues[currentUpgradeIndex]);
                break;
            case "Flippers":
                gameManager.SetSwimSpeed(upgradeValues[currentUpgradeIndex]);
                break;
        }
    }
}
