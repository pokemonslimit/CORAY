using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour {

	public RayManagerController rayManager;
	public float moveSpeed = 10.0f;

	private Rigidbody rigidBody;



	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		
	}

	// Update is called once per frame
	void Update () {
		// Test fire!
		if (Input.GetKeyDown (KeyCode.Z)) {
			rayManager.FireRay (gameObject.transform.position, gameObject.transform.forward, 1);
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			rayManager.FireRay (gameObject.transform.position, gameObject.transform.forward, 2);
		}
		// control
		rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;
		if (rigidBody.velocity != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (rigidBody.velocity, Vector3.up);
		}

	}
}
