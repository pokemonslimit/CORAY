using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayManagerController : MonoBehaviour {

	public GameObject rayCandidate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FireRay(Vector3 position, Vector3 direction, int id){
		var rayObject = Instantiate (rayCandidate);
		rayObject.transform.parent = gameObject.transform;
		var rayController = rayObject.GetComponent<RaySegmentController> ();
		rayController.InitPrimaryRay (position, direction, id);
	}
}
