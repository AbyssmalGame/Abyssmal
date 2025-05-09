using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingRangedAttacker : SwimmingEnemy
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private float attackChargeTime = 2.0f;
    [SerializeField] private float attackSpeed = 12.0f;
    [SerializeField] private float attackDuration = 1.0f;

    [SerializeField] private float attackLagTime = 0.2f;

    private ParticleSystem rangedAttackParticles;

    protected override void OnStart()
    {
        base.OnStart();
        rangedAttackParticles = GetComponentInChildren<ParticleSystem>();
        rangedAttackParticles.Stop();
    }

    protected override void LockOn()
    {
        RaycastHit hit;

        Vector3 hitRay = transform.position - (transform.forward * hostileEnemySwimmingMovement.raycastForwardOffset);
        Debug.DrawRay(hitRay, transform.forward * attackRange, Color.red);

        if (Physics.Raycast(hitRay, transform.forward, out hit, attackRange) && hit.collider.gameObject == target)
        {
            Attack();
            audioSource.PlayOneShot(lockOnSound);
        }
    }

    protected override void Attack()
    {
        StartCoroutine("RayAttack");
    }

    IEnumerator RayAttack()
    {
        hostileEnemySwimmingMovement.isAttacking = true;
        hostileEnemySwimmingMovement.isMoving = false;
        hostileEnemySwimmingMovement.isIdle = false;

        float attackChargeTimePassed = 0;
        float attackDurationTimePassed = 0;

        while (attackChargeTimePassed <= attackChargeTime)
        {
            DecelerateByFrame();
            Vector3 pos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = rotation;
            attackChargeTimePassed += Time.deltaTime;
            yield return null;
        }

        rangedAttackParticles.Play();

        while (attackDurationTimePassed <= attackDuration)
        {
            Vector3 pos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = rotation;
            attackDurationTimePassed += Time.deltaTime;
            yield return null;
        }
        rangedAttackParticles.Stop();
        yield return new WaitForSeconds(attackLagTime);

        hostileEnemySwimmingMovement.isAttacking = false;
    }
}