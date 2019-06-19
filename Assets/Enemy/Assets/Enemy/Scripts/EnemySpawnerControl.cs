using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerControl : MonoBehaviour {

	public List<GameObject> EnemyCandidate;
	public List<Transform> SpawnPoint;
	public GameObject initFollowTarget;


	public float SpawnEnemyTime = 1;
	private float spwanCounter = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		spwanCounter += Time.deltaTime;

		if (spwanCounter >= SpawnEnemyTime) {
			spwanCounter = 0;
			GameObject newEnemy = GameObject.Instantiate (EnemyCandidate[Random.Range (0, EnemyCandidate.Count)]);
			newEnemy.GetComponent<EnemyController> ().FollowTarget = initFollowTarget;
			newEnemy.transform.position = SpawnPoint [Random.Range (0, SpawnPoint.Count)].position;

		}

		
	}
}
