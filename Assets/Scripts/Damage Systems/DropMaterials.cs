using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class DropMaterials : MonoBehaviour
{
    public ResultsManager resultsManager;
    public int minGold = 0;
    public int maxGold = 0;
    public int minIron = 0;
    public int maxIron = 0;
    public int minDebris = 0;
    public int maxDebris = 0;

    public void doDrops()
    {
        int gold = Random.Range(minGold, maxGold);
        int iron = Random.Range(minIron, maxIron);
        int debris = Random.Range(minDebris, maxDebris);

        Debug.Log("GOLD: " + gold + " | IRON: " + iron + " | DEBRIS: " + debris);

        resultsManager.ObtainGold(gold);
        resultsManager.ObtainIron(iron);
        resultsManager.ObtainDebris(debris);

    }
}
