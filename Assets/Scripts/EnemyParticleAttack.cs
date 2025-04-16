using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttack : MonoBehaviour
{

    [SerializeField] private float attackCollisionDelay = 0.5f;

    private float attackCollisionDelayTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnParticleCollision(GameObject other)    
    {
        if (other.CompareTag("Player") && attackCollisionDelayTimer >= attackCollisionDelay)
        {
            Debug.Log("Particle hit player!");
            attackCollisionDelayTimer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCollisionDelayTimer <= attackCollisionDelay)
        {
            attackCollisionDelayTimer += Time.deltaTime;
        }
    }

}
