using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HostileGroundEnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject targetGameObject;

    private NavMeshAgent navMeshAgent;
    private Vector3 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentTarget = targetGameObject.transform.position;
        navMeshAgent.SetDestination(currentTarget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
