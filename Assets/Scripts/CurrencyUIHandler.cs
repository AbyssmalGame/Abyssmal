using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyUIHandler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI goldText;
    public TMPro.TextMeshProUGUI ironText;
    public TMPro.TextMeshProUGUI debrisText;
    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }
    public void updateCurrencyUI()
    {
        goldText.text = gameManager.gold.ToString();
        ironText.text = gameManager.iron.ToString();
        debrisText.text = gameManager.debris.ToString();
    }


}
