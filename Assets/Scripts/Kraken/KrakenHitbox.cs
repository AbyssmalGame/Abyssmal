using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenHitbox : MonoBehaviour
{
    public int damageAmount = 80;
    [SerializeField] private CapsuleCollider colliderComponent;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attack1AllTentacleStab hits!");
            //Physics.IgnoreCollision(GetComponent<Collider>(), other);
            other.GetComponent<PlayerStatManager>()?.TakeDamage(damageAmount);
        }
        colliderComponent.enabled = false;
    }
}
