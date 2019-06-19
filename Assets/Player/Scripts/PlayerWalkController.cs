using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkController : MonoBehaviour {

	public float moveSpeed = 5.0f;
	//public float drag = 0.5f;
	public float terminalRotationSpeed = 25.0f;
	public Vector3 MoveVector{ set; get;}
	public JoyStick2 VirtualJoyStick;
	private float nowtan;
	public RayManagerController rayController;
	public int play_id;

	private Rigidbody thisRigidbody;

	// Use this for initialization
	void Start () {
		thisRigidbody = this.gameObject.GetComponent<Rigidbody> ();
		thisRigidbody.maxAngularVelocity = terminalRotationSpeed;
		//thisRigidbody.drag = drag;
		nowtan = this.transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		MoveVector = PoolInput ();
		Move();
	}

	private void Move(){
		thisRigidbody.velocity  = (MoveVector * moveSpeed);
		if (MoveVector.magnitude != 0)
			nowtan = -(Mathf.Atan2 (MoveVector.z, MoveVector.x) * Mathf.Rad2Deg)+90f;

		//nowtan = (nowtan < 0) ? (nowtan + 360f) : nowtan;
		this.transform.eulerAngles =  new Vector3(0, nowtan, 0);
		//Debug.Log (nowtan);
	}

	private Vector3 PoolInput(){
		Vector3 dir = Vector3.zero;
		dir.x = VirtualJoyStick.Horizontal ();
		dir.z = VirtualJoyStick.Vertical();

		if(dir.magnitude > 1)
			dir.Normalize();

		return dir;
	}

	private void Attack(float damage){
		rayController.FireRay (transform.position + transform.forward * 0.5f, transform.forward, play_id);
	}

}
