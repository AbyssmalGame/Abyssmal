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
    [SerializeField] private AudioClip bulletHitSound;
    private AudioSource audioSource;

    private void Start()
    {
        currentHP = maxHP;
        OnDiecallback = gameObject.GetComponent<DropMaterials>().doDrops;
        audioSource = GameObject.Find("GamePlayer").GetComponent<AudioSource>();
    }

    public void ApplyDamage(int damage)
    {
        currentHP -= damage;
        audioSource.PlayOneShot(bulletHitSound);
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
        Color originalColor = Color.white;
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
