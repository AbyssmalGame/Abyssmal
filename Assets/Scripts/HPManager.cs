using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public int maxHP;
    public void ApplyDamage(int damage)
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
