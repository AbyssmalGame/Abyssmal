using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatValues : MonoBehaviour
{
    public enum PlayerHP : int
    {
        lv1 = 100,
        lv2 = 120,
        lv3 = 150,
        lv4 = 200
    }
    public ArrayList<float> Oxygen = [600f, 660f, 720f, 840f]    
    public enum Oxygen : int
    {
        lv1 = 600,
        lv2 = 660,
        lv3 = 720,
        lv4 = 840
    }
    public enum SwimSpeeed : int
    {
        lv1 = 1,
        lv2 = 2,
        lv3 = 3,
        lv4 = 4
    }
}
