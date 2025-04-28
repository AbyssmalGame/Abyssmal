using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenHitbox : MonoBehaviour
{
    public int damageAmount = 80;
    [SerializeField] private CapsuleCollider colliderComponent;

    public bool isHitboxActive = false;
    public bool isHitOnce = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isHitboxActive && !isHitOnce)
        {
            Debug.Log("Attack1AllTentacleStab hits!");
            other.GetComponent<PlayerStatManager>()?.TakeDamage(damageAmount);
            isHitOnce = true;
        }
    }
}
