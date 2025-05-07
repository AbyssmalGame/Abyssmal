using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public float spawnInterval = 10f;
    private int limitCounter = 0;
    private int spawnLimit = 6;
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        if (limitCounter >= spawnLimit) return;
		GameObject instance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

		// Assign the player reference if the prefab has a script that needs it
		HostileEnemySwimmingMovement enemyScript = instance.GetComponent<HostileEnemySwimmingMovement>();
		if (enemyScript != null)
		{
			enemyScript.target = player;
		}
        limitCounter++;
	}
}
