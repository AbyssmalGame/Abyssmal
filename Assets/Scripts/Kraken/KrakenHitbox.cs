using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KrakenHitbox : MonoBehaviour
{
    public int damageAmount = 40;

    public bool isHitboxActive = false;
    public bool isHitOnce = false;

    private PlayerStatManager statManager;

    private void Start()
    {
        GameObject player = GameObject.Find("GamePlayer");
        statManager = player.GetComponent<PlayerStatManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isHitboxActive && !isHitOnce)
        {
            Debug.Log("Attack1AllTentacleStab hits!");
            statManager?.TakeDamage(damageAmount);
            isHitOnce = true;
        }
    }
}
