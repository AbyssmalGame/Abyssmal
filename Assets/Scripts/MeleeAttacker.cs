using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private float decelerationSpeed = 0.1f;
    [SerializeField] private float attackChargeTime = 1.0f;
    [SerializeField] private float attackSpeed = 10.0f;

    [SerializeField] private float attacklagTime = 1.0f;

    private HostileEnemySwimmingMovement hostileEnemySwimmingMovement;
    private GameObject target;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        hostileEnemySwimmingMovement = GetComponent<HostileEnemySwimmingMovement>();
        rb = GetComponent<Rigidbody>();
        target = hostileEnemySwimmingMovement.target;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hostileEnemySwimmingMovement.isAttacking)
        {
            LockOn();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            Debug.Log("hit target!");
        }
    }

    private void LockOn()
    {
        RaycastHit hit;

        Vector3 hitRay = transform.position - (transform.forward * hostileEnemySwimmingMovement.raycastForwardOffset);
        Debug.DrawRay(hitRay, transform.forward * attackRange, Color.red);

        if (Physics.Raycast(hitRay, transform.forward, out hit, attackRange) && hit.collider.gameObject == target)
        {
            StartCoroutine("StraightAttack");
        }
    }

    private void DecelerateByFrame()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            rb.AddForce(-rb.velocity.normalized * decelerationSpeed, ForceMode.Force);
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }
    private void Decelerate()
    {
        while (rb.velocity != Vector3.zero)
        {
            DecelerateByFrame();
        }
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
        rb.angularVelocity = Vector3.zero;

        Decelerate();

        // Causing issues, possibly multiple coroutines which is crashing Unity
        //hostileEnemySwimmingMovement.isAttacking = false;
    }
}