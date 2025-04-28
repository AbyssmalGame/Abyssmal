using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStat : ScriptableObject
{
    public float currentValue;
    public float maxValue;

    public virtual void Initialize(float value)
    {
        maxValue = value;
        currentValue = value;
    }

    public void ResetStat() => currentValue = maxValue;
}
