using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HPManager : MonoBehaviour
{
    public Action OnDiecallback = null;
    public int maxHP;
    private int currentHP;

    [SerializeField] private Color32 damageColor = new Color(255, 0, 0, 150);
    [SerializeField] private AudioClip bulletHitSound;
    private AudioSource audioSource;

    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        currentHP = maxHP;
        OnDiecallback = gameObject.GetComponent<DropMaterials>().doDrops;
        audioSource = GameObject.Find("GamePlayer").GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        int hpBeforeHit = currentHP;
        currentHP -= damage;
        audioSource.PlayOneShot(bulletHitSound);
        if (hpBeforeHit > 0)
        {
            DamageFlash();
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        OnDiecallback?.Invoke();
        currentHP = 0;
        if (gameObject.tag != "Player")
        {
            StartCoroutine(UpsideDownDeath());
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
        SwimmingEnemy swimmingEnemy = GetComponent<SwimmingEnemy>();
        KrakenAttacker krakenAttacker = GetComponent<KrakenAttacker>();
        if (swimmingEnemy != null)
        {
            swimmingEnemy.StopAllCoroutines();
            swimmingEnemy.isDead = true;
            swimmingEnemy.enabled = false;
            HostileEnemySwimmingMovement hostileEnemySwimmingMovement = GetComponent<HostileEnemySwimmingMovement>();
            hostileEnemySwimmingMovement.StopAllCoroutines();
            hostileEnemySwimmingMovement.enabled = false;

            ParticleSystem enemyParticleAttack = GetComponent<ParticleSystem>();
            if (enemyParticleAttack != null)
            {
                GameObject particle = transform.GetChild(1).gameObject;
                Destroy(particle);
            }
            if (krakenAttacker != null)
            {
                krakenAttacker.StopAllCoroutines();
                krakenAttacker.enabled = false;
                rb.AddForce(Vector3.down * 2.5f, ForceMode.Impulse);
            } else
            {
                rb.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
            }
        }
        else
        {
            GroundEnemy groundEnemy = GetComponent<GroundEnemy>();
            groundEnemy.StopAllCoroutines();
            groundEnemy.isDead = true;
            groundEnemy.enabled = false;
            HostileGroundEnemyMovement hostileGroundEnemyMovement = GetComponent<HostileGroundEnemyMovement>();
            hostileGroundEnemyMovement.StopAllCoroutines();
            hostileGroundEnemyMovement.enabled = false;
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }
        
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = krakenAttacker == null ? Quaternion.Euler(0f, 0f, 180f) : Quaternion.Euler(0f, 0f, 0f);

        float rotationDuration = 1.5f;
        float elapsedTime = 0f;
        float fadeDuration = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

        if (krakenAttacker == null)
        {
            fadeDuration = 1.5f;
            StartCoroutine(FadeAway(fadeDuration, 0.08f));
        }
        else
        {
            GameObject spawner = GameObject.Find("Spawner");
            Destroy(spawner);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                HPManager hpManager = enemy.GetComponent<HPManager>();
                if (hpManager.currentHP > 0)
                {
                    hpManager.Die();
                }
            }

            fadeDuration = 2.5f;
            StartCoroutine(FadeAway(fadeDuration, 0.004f));
            float animatorTime = 0f;
            while (animatorTime <= 5f && animator.speed > 0)
            {
                animator.speed -= 0.004f;
                animatorTime += Time.deltaTime;
                yield return null;
            }
            animator.speed = 0f;
        }

        Destroy(gameObject, fadeDuration);
    }
    IEnumerator FadeAway(float fadeDuration, float fadeIncrement)
    {
        float elapsedTime = 0;
        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        while (elapsedTime < fadeDuration)
        {
            foreach (Renderer r in renderers)
            {
                foreach (Material material in r.materials)
                {
                    material.SetFloat("_Surface", 1.0f);
                    material.SetFloat("_Blend", 0.0f);
                    Color c = material.color;
                    c.a -= fadeIncrement;
                    material.color = c;

                    material.SetFloat("_ReceiveShadows", 0.0f);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                }
            }
            yield return null;
        }
    }
}
