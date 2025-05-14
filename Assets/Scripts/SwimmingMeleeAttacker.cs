using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingMeleeAttacker : SwimmingEnemy
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private float attackChargeTime = 2.0f;
    [SerializeField] private float attackSpeed = 12.0f;

    [SerializeField] private float attackCollisionDelay = 0.5f;
    [SerializeField] private float attackLagTime = 0.2f;

    private float attackCollisionDelayTimer = 0.0f;

    [SerializeField] private AudioClip straightAttackSound;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (attackCollisionDelayTimer <= attackCollisionDelay)
        {
            attackCollisionDelayTimer += Time.deltaTime;
        }
    }

    //Attack Logic Here
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackCollisionDelayTimer >= attackCollisionDelay && !isDead)
        {
            rb.angularVelocity = Vector3.zero;
            Debug.Log("hit target!");
            //Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
            target.GetComponent<PlayerStatManager>()?.TakeDamage(damageAmount);
            attackCollisionDelayTimer = 0f;
        }
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
        StartCoroutine("StraightAttack");
    }

    IEnumerator StraightAttack()
    {
        hostileEnemySwimmingMovement.isAttacking = true;
        hostileEnemySwimmingMovement.isMoving = false;
        hostileEnemySwimmingMovement.isIdle = false;

        float attackChargeTimePassed = 0;

        while (attackChargeTimePassed <= attackChargeTime)
        {
            DecelerateByFrame();
            Vector3 pos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = rotation;
            attackChargeTimePassed += Time.deltaTime;
            yield return null;
        }

        rb.AddForce(transform.forward * attackSpeed, ForceMode.Impulse);
        audioSource.PlayOneShot(straightAttackSound);
        rb.freezeRotation = true;
        yield return new WaitForSeconds(attackLagTime);

        float timer = 0;

        while (rb.velocity != Vector3.zero)
        {
            DecelerateByFrame();
            timer += Time.deltaTime;
            if (timer >= attackLagTime)
            {
                rb.velocity = Vector3.zero;
            }
            yield return null;
        }

        rb.freezeRotation = false;
        hostileEnemySwimmingMovement.isAttacking = false;
    }
}