using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stats/Results")]
public class ResultsManager : ScriptableObject
{
    public int lastSceneIndex = -1;
    public int obtainedGold = 0;
    public int obtainedIron = 0;
    public int obtainedDebris = 0;

    public void ObtainGold(int gold)
    {
        obtainedGold += gold;
    }
    public void ObtainIron(int iron)
    {
        obtainedIron += iron;
    }
    public void ObtainDebris(int debris)
    {
        obtainedDebris += debris;
    }

    public void addToGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddGold(obtainedGold);
            GameManager.Instance.AddIron(obtainedIron);
            GameManager.Instance.AddDebris(obtainedDebris);

            obtainedGold = 0;
            obtainedIron = 0;
            obtainedDebris = 0;
        }
    }
}
