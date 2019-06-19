using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteractionController : MonoBehaviour {

	public float energyCost = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual bool DoRayAction(RaySegmentController segment, RaycastHit hitInfo, Vector3 inDirection){
		return true; // by default, terminate the ray
	}
}
