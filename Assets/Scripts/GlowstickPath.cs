using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowstickPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] glowsticks = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            glowsticks.SetValue(transform.GetChild(i), i);
        }
        for (int i = 0; i < glowsticks.Length - 1; i++)
        {
            glowsticks[i].transform.SetParent(glowsticks[i + 1].transform, true);
        }
    }
}
