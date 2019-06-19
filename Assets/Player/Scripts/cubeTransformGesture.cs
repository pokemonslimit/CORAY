using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class cubeTransformGesture : MonoBehaviour {

	public TransformGesture transformGesture;
	private Rigidbody rigidBody;
	private Collider collider;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody> ();
		collider = this.GetComponent<BoxCollider> ();

		transformGesture.TransformStarted += (object sender, System.EventArgs e) => {
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;
			collider.enabled = false;
		};
		transformGesture.Transformed += (object sender, System.EventArgs e) => {
			this.transform.Rotate (new Vector3 (0, 1, 0), transformGesture.DeltaRotation);
			Debug.Log( transformGesture.DeltaPosition);
		};
		transformGesture.TransformCompleted += (object sender, System.EventArgs e) => {
			rigidBody.useGravity = true;
			collider.enabled = true;
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
