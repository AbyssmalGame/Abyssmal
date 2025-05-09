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

    private Rigidbody rb;

    private void Start()
    {
        currentHP = maxHP;
        OnDiecallback = gameObject.GetComponent<DropMaterials>().doDrops;
        audioSource = GameObject.Find("GamePlayer").GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
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
        if (gameObject.tag != "Player")
        {
            StartCoroutine(UpsideDownDeath());
            Destroy(gameObject, 6.5f);
        }
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
        if (currentHP > 0)
        {
            foreach (Material material in r.materials)
            {
                material.color = originalColor;
            }
        }
    }

    IEnumerator UpsideDownDeath()
    {
        rb.AddForce(Vector3.up * 1.5f, ForceMode.Force);
        GetComponent<SwimmingEnemy>().enabled = false;
        GetComponent<HostileEnemySwimmingMovement>().enabled = false;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);

        float rotationDuration = 1.5f;
        float fadeDuration = 3.0f;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

        yield return new WaitForSeconds(2.0f);

        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();

        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            foreach (Renderer r in renderers)
            {
                foreach (Material material in r.materials)
                {
                    Color c = material.color;
                    c.a -= 0.01f;
                    material.color = c;
                }
            }
            yield return null;
        }
    }
}
