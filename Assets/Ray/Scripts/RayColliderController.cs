using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayColliderController : MonoBehaviour {

	public RaySegmentController segment;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		
		var hitController = collider.gameObject.GetComponent<RayHitController>();
		if (hitController != null) {
			segment.OnHitTarget (hitController);
		}
	}
}
