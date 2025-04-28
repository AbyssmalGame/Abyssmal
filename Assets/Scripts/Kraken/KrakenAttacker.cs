using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenAttacker : SwimmingEnemy
{

    [SerializeField] private float attackRotationSpeed = 2f;
    [SerializeField] private float attack1RangeAllTentacleStab = 5f;

    private Animator animator;

    // Start is called before the first frame update
    protected override void OnStart()
    {
        base.OnStart();
        animator = GetComponent<Animator>();
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
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).length;

        animator.SetTrigger("Attack1AllTentacleStab");

        float attackTimePassed = 0;
        Quaternion targetRotation = Quaternion.Euler(0, 180, 0);

        while (attackTimePassed <= animationTime / 2)
        {
            DecelerateByFrame();
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, attackRotationSpeed * Time.deltaTime);
            attackTimePassed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(animationTime);

        hostileEnemySwimmingMovement.isAttacking = false;
    }
}
