using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public Action OnDiecallback = null;
    public int maxHP;
    private int currentHP;

    [SerializeField] private Color32 damageColor = new Color(255, 0, 0, 150);

    private void Start()
    {
        currentHP = maxHP;
        OnDiecallback = gameObject.GetComponent<DropMaterials>().doDrops;
    }

    public void ApplyDamage(int damage)
    {
        currentHP -= damage;
        DamageFlash();
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

    private void DamageFlash()
    {
        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            StartCoroutine(RedFlash(r));
        }
    }

    IEnumerator RedFlash(Renderer r)
    {
        Color originalColor = r.material.color;
        foreach (Material material in r.materials)
        {
            material.color = damageColor;
        }
        yield return new WaitForSeconds(0.15f);
        foreach (Material material in r.materials)
        {
            material.color = originalColor;
        }
    }
}
