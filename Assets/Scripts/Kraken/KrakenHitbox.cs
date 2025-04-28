using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenHitbox : MonoBehaviour
{

    [SerializeField] private CapsuleCollider colliderComponent;

    public bool isHitboxActive = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isHitboxActive)
        {
            Debug.Log("Attack1AllTentacleStab hits!");
        }
    }
}
