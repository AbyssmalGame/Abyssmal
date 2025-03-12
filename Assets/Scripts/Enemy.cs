using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move(GameObject target);

    public abstract void Attack(GameObject target);

    public abstract void Die();
}
