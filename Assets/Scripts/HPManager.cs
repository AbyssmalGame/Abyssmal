using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public Action OnDiecallback = null;
    public int maxHP;
    public void ApplyDamage(int damage)
    {
        maxHP -= damage;
        
    }

    public void Die()
    {
        OnDiecallback.Invoke();
        Destroy(gameObject);
    }
}
