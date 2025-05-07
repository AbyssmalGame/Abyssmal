using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEverySecond : MonoBehaviour
{

    [SerializeField] private float timeBetweenDamage = 3.0f;
    [SerializeField] private int damageAmount = 1;

    private HPManager hpManager;

    // Start is called before the first frame update
    void Start()
    {
        hpManager = GetComponent<HPManager>();
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenDamage);
            hpManager.ApplyDamage(damageAmount);
        }
    }
}
