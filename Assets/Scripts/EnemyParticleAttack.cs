using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttack : MonoBehaviour
{

    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Project hit player!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
