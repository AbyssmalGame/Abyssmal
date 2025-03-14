using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemySwimmingMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private float rotationalDamp = 10f;
    [SerializeField] private float obstacleAvoidanceRotation = 25f;

    [Header("Vision Settings")]
    [SerializeField] private GameObject target;
    [SerializeField] private float detectionDistance = 6.0f;
    [SerializeField] private float raycastForwardOffset = 0.1f;
    [SerializeField] private float leftRaycastOffset = 0.5f;
    [SerializeField] private float rightRaycastOffset = 0.5f;
    [SerializeField] private float upRaycastOffset = 0.5f;
    [SerializeField] private float downRaycastOffset = 0.5f;

    [Header("Idle Settings")]
    [SerializeField] private float idleMovementSpeed = 0.1f;
    [SerializeField] private float hostileDetectionDistance = 20.0f;
    [SerializeField] private float minimumIdleTime = 0f;
    [SerializeField] private float maximumIdleTime = 10.0f;
    [SerializeField] private float minimumNextDistance = 1.0f;
    [SerializeField] private float maximumNextDistance = 3.0f;

    private Rigidbody rb;
    private Vector3 currentTarget;

    private bool playerDetected = false;
    private bool isIdle = false;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTarget = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerDetected = Vector3.Distance(target.transform.position, transform.position) < hostileDetectionDistance ? true : false;
        if (!playerDetected && !isIdle)
        {
            StartCoroutine("IdleMove");
        }
        else if (playerDetected)
        {
            currentTarget = target.transform.position;
            isIdle = false;
            Move();
        } else if (isMoving && isIdle)
        {
            Move();
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, currentTarget) > 0.2)
        {
            PathFinding();
            if (isIdle)
            {
                rb.MovePosition(rb.position + idleMovementSpeed * Time.fixedDeltaTime * transform.forward);
            } else
            {
                rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * transform.forward);
            }
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        } else
        {
            isMoving = false;
            isIdle = false;
        }
    }

    void Turn()
    {
        Vector3 pos = currentTarget - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 movementOffset = Vector3.zero;

        Vector3 left = transform.position - (transform.forward * raycastForwardOffset) - transform.right * leftRaycastOffset;
        Vector3 right = transform.position - (transform.forward * raycastForwardOffset) + transform.right * rightRaycastOffset;
        Vector3 up = transform.position - (transform.forward * raycastForwardOffset) + transform.up * upRaycastOffset;
        Vector3 down = transform.position - (transform.forward * raycastForwardOffset) - transform.up * downRaycastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.blue);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.blue);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.blue);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.blue);

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance) && hit.collider.gameObject != target)
        {
            movementOffset += Vector3.right;
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance) && hit.collider.gameObject != target)
        {
            movementOffset -= Vector3.right;
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance) && hit.collider.gameObject != target)
        {
            movementOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance) && hit.collider.gameObject != target)
        {
            movementOffset += Vector3.up;
        }

        if (movementOffset != Vector3.zero)
        {
            transform.Rotate(movementOffset * obstacleAvoidanceRotation * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }

    void SetIdleTarget()
    {
        float randomX = Random.Range(transform.position.x - minimumNextDistance, transform.position.x + maximumNextDistance);
        float randomY = Random.Range(transform.position.y - minimumNextDistance, transform.position.y + maximumNextDistance);
        float randomZ = Random.Range(transform.position.z - minimumNextDistance, transform.position.z + maximumNextDistance);
        Vector3 randomTarget = new Vector3(randomX, randomY, randomZ);
        currentTarget = randomTarget;
    }

    IEnumerator IdleMove()
    {
        isIdle = true;
        int idleTimePassed = 0;
        float idleTime = Random.Range(minimumIdleTime, maximumIdleTime);
        while (!playerDetected && idleTimePassed <= idleTime)
        {
            yield return new WaitForSeconds(1);
            idleTimePassed++;
        }
        if (playerDetected)
        {
            yield break;
        } else
        {
            SetIdleTarget();
            isMoving = true;
        }
    }
}
