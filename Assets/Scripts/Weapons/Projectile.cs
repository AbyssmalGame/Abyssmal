using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject src = null;
    public int damage = 0;

    void Awake()
    {
        if (src == null)
        {
            StartCoroutine(TimedDestroy.DestroyAfterSeconds(2f, gameObject));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "enemy")
        {
            other.GetComponent<HPManager>()?.ApplyDamage(damage);
        }


    }

}
