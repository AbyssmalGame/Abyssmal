using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMeleeAttacker : GroundEnemy
{

    [SerializeField] private int damageAmount = 5;

    [SerializeField] private float attackCollisionDelay = 0.5f;

    private float attackCollisionDelayTimer = 0.0f;
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (attackCollisionDelayTimer <= attackCollisionDelay)
        {
            attackCollisionDelayTimer += Time.deltaTime;
        }
    }

    //Attack Logic Here
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == target && attackCollisionDelayTimer >= attackCollisionDelay)
        {
            attackCollisionDelayTimer = 0f;
            target.GetComponent<PlayerStatManager>().TakeDamage(damageAmount);
        }
    }
}
