using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public int maxHp;
	public int currentHp;
	public int type;

	public Text hpText;
	public GameObject FollowTarget;

	// Use this for initialization
	void Start () {
		currentHp = maxHp;
		UpdateHpText ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowTarget != null) {
			Vector3 lookAt = FollowTarget.gameObject.transform.position;
			//transform.LookAt (lookAt);
			Vector3 targetDirection = (FollowTarget.gameObject.transform.position - transform.position).normalized;
			transform.position += targetDirection * Time.deltaTime * 1;
		}
	}

	void HitByRays(HashSet<RaySegmentController> hitRays)
	{
		foreach(RaySegmentController ray in hitRays){
			if (ray.ray_id == type) {
				currentHp -= 1; //controller fire
				UpdateHpText ();
				if (currentHp <= 0) {
					Destroy (gameObject);
				}
			}
		}
	}

	void UpdateHpText(){
		hpText.text = "HP = " + currentHp;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Tower")) {
			other.gameObject.SendMessage ("OnEnemyTouch");
			GameObject.Destroy (this.gameObject);
		}
	}

}
