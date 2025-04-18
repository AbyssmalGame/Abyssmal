using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : MonoBehaviour
{

    protected HostileGroundEnemyMovement hostileGroundEnemyMovement;
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
        hostileGroundEnemyMovement = GetComponent<HostileGroundEnemyMovement>();
        rb = GetComponent<Rigidbody>();
        target = hostileGroundEnemyMovement.target;
    }

    protected virtual void OnUpdate()
    {

    }
}
