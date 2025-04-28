using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public Action OnDiecallback = null;
    public int maxHP;
    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
        OnDiecallback = gameObject.GetComponent<DropMaterials>().doDrops;
    }

    public void ApplyDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDiecallback?.Invoke();
        if (gameObject.tag != "Player")   Destroy(gameObject);
    }
}
