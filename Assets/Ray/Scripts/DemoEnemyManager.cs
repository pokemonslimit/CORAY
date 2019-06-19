using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnemyManager : MonoBehaviour {

	public GameObject enemyCandidate;
	public float spwanTime = 10.0f;

	private float currentSpawnTime;

	// Use this for initialization
	void Start () {
		currentSpawnTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpawnTime += Time.deltaTime;
		if (currentSpawnTime >= spwanTime) {
			currentSpawnTime = 0.0f;
			SpawnEnemy ();
		}
	}

	void SpawnEnemy(){
		var enemy = Instantiate (enemyCandidate);
		enemy.transform.parent = transform;
		enemy.transform.position = new Vector3 ((float)Random.value * 14 - 7, (float)Random.value * 5 - 2.5f ,0);
	}
}
