using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostileGroundEnemyMovement : MonoBehaviour
{

    [SerializeField] public GameObject target;

    private NavMeshAgent navMeshAgent;
    private Vector3 currentTarget;

    private Animator animator;

    [SerializeField] private float minimumIdleTime = 10.0f;
    [SerializeField] private float maximumIdleTime = 20.0f;
    [SerializeField] private float minimumNextDistance = 5.0f;
    [SerializeField] private float maximumNextDistance = 15.0f;

    private FieldOfView fieldOfView;
    private bool isChasing = false;
    private bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
        animator = GetComponentInChildren<Animator>();
        currentTarget = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null && fieldOfView.canSeePlayer)
        {
            currentTarget = target.transform.position;
            navMeshAgent.SetDestination(currentTarget);
            isChasing = true;
            isIdle = false;
        }
        else if (!fieldOfView.canSeePlayer && !isIdle)
        {
            StartCoroutine("IdleMove");
        } 

        if (!isChasing && navMeshAgent.velocity.magnitude == 0)
        {
            animator.speed = 0;
        } else
        {
            animator.speed = 1;
        }
    }
    void SetIdleTarget()
    {
        Vector3 randomDirection = Random.insideUnitCircle * Random.Range(maximumNextDistance, minimumNextDistance);
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maximumNextDistance, 1);
        Vector3 finalPosition = hit.position;
        currentTarget = finalPosition;
        navMeshAgent.SetDestination(currentTarget);
        isIdle = false;
    }

    IEnumerator IdleMove()
    {
        int idleTimePassed = 0;
        float idleTime = Random.Range(minimumIdleTime, maximumIdleTime);
        isChasing = false;
        isIdle = true;
        while (!isChasing && idleTimePassed <= idleTime)
        {
            yield return new WaitForSeconds(1);
            idleTimePassed++;
        }
        if (isChasing)
        {
            yield break;
        }
        else
        {
            SetIdleTarget();
        }
    }
}
