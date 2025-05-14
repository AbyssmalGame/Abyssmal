using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenAttacker : SwimmingEnemy
{

    [SerializeField] private float attackRotationSpeed = 0.75f;
    [SerializeField] private float attack1RangeAllTentacleStab = 5f;
    
    [SerializeField] private CapsuleCollider attack1AllTentacleStabHitbox;
    [SerializeField] private KrakenHitbox attack1AllTentacleStabHitboxComponent;

    private Animator animator;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        base.OnStart();
        animator = GetComponent<Animator>();
        StartCoroutine(StartRoar());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void LockOn()
    {
        RaycastHit hit1;

        Vector3 hitRay = transform.position - (transform.forward * hostileEnemySwimmingMovement.raycastForwardOffset);
        Debug.DrawRay(hitRay, transform.forward * attack1RangeAllTentacleStab, Color.red);

        if (Physics.Raycast(hitRay, transform.forward, out hit1, attack1RangeAllTentacleStab) && hit1.collider.gameObject == target)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        if (hostileEnemySwimmingMovement.isAttacking)
        {
            return;
        }

        hostileEnemySwimmingMovement.isAttacking = true;
        hostileEnemySwimmingMovement.isMoving = false;
        hostileEnemySwimmingMovement.isIdle = false;

        StartCoroutine(attack1AllTentacleStab());
    }

    private IEnumerator attack1AllTentacleStab()
    {

        audioSource.PlayOneShot(lockOnSound);
        animator.SetTrigger("Attack1AllTentacleStab");

        float animationTime = 4.2f;

        float attackTimePassed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

        while (attackTimePassed <= attackRotationSpeed)
        {
            DecelerateByFrame();
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, attackTimePassed / attackRotationSpeed);
            attackTimePassed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(animationTime - attackTimePassed);
        Debug.Log("Attack1AllTentacleStab Hitbox");

        attack1AllTentacleStabHitboxComponent.isHitboxActive = true;
        yield return new WaitForSeconds(1);
        attack1AllTentacleStabHitboxComponent.isHitboxActive = false;
        attack1AllTentacleStabHitboxComponent.isHitOnce = false;

        hostileEnemySwimmingMovement.isAttacking = false;
    }

    private IEnumerator StartRoar()
    {
        yield return new WaitForSeconds(3);
        audioSource.Play();
    }
}
