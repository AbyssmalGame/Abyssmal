using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwimmingEnemy : MonoBehaviour
{

    [SerializeField] protected float decelerationSpeed = 1.0f;

    protected HostileEnemySwimmingMovement hostileEnemySwimmingMovement;
    protected GameObject target;

    protected Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnStart()
    {
        hostileEnemySwimmingMovement = GetComponent<HostileEnemySwimmingMovement>();
        rb = GetComponent<Rigidbody>();
        target = hostileEnemySwimmingMovement.target;
    }

    protected virtual void OnUpdate()
    {
        if (!hostileEnemySwimmingMovement.isAttacking)
        {
            LockOn();
        }
    }

    protected abstract void LockOn();

    protected abstract void Attack();

    protected virtual void DecelerateByFrame()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            rb.AddForce(-rb.velocity.normalized * decelerationSpeed, ForceMode.Force);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

}
