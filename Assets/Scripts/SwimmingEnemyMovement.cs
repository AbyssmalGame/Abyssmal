using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationalDamp = 25f;
    [SerializeField] float obstacleAvoidanceRotation = 25f;

    [SerializeField] float detectionDistance = 7.5f;

    [SerializeField] float leftRaycastOffset = 0.5f;
    [SerializeField] float rightRaycastOffset = 0.5f;
    [SerializeField] float upRaycastOffset = 0.5f;
    [SerializeField] float downRaycastOffset = 0.5f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PathFinding();
        Move();
        rb.angularVelocity = Vector3.zero;
    }

    void Turn()
    {
        Vector3 pos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 movementOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * leftRaycastOffset;
        Vector3 right = transform.position + transform.right * rightRaycastOffset;
        Vector3 up = transform.position + transform.up * upRaycastOffset;
        Vector3 down = transform.position - transform.up * downRaycastOffset;

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
}
