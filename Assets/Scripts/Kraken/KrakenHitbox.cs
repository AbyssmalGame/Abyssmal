using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenHitbox : MonoBehaviour
{

    [SerializeField] private CapsuleCollider colliderComponent;

    private float attackCollisionDelayTimer = 0.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attack1AllTentacleStab hits!");
        }
        colliderComponent.enabled = false;
    }
}
