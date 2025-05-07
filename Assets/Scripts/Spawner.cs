using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public float spawnInterval = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), 0f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnPrefab()
    {
		GameObject instance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

		// Assign the player reference if the prefab has a script that needs it
		HostileEnemySwimmingMovement enemyScript = instance.GetComponent<HostileEnemySwimmingMovement>();
		if (enemyScript != null)
		{
			enemyScript.target = player;
		}
	}
}
