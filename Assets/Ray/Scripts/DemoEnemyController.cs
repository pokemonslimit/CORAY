using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoEnemyController : MonoBehaviour {

	public int maxHp = 1;
	public int currentHp;

	public Text hpText;

	// Use this for initialization
	void Start () {
		currentHp = maxHp;
		UpdateHpText ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void HitByRays(HashSet<RaySegmentController> hitRays){
		Debug.Log ("Hit by rays: " + hitRays.Count);
		currentHp -= hitRays.Count;
		UpdateHpText ();
		if (currentHp <= 0) {
			Destroy (gameObject);
		}
	}

	void UpdateHpText(){
		hpText.text = "HP = " + currentHp;
	}
}
