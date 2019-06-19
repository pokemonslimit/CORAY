using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject RotateCenter;
	//public float RotateSpeed;
	public JoyStickController moveJoystick;

	public RayManagerController rayController;
	public int play_id;

	// Use this for initialization
	void Start () {
		//Player = this.GetComponent<GameObject> ();	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (moveJoystick.premovetan + " " + moveJoystick.nowtan);
		if (moveJoystick.movetan != 0 && !moveJoystick.cantmove) {
			transform.RotateAround (RotateCenter.transform.position, Vector3.forward, moveJoystick.movetan);
		}
		moveJoystick.movetan = 0;
	}

	private void Attack(float damage){
		rayController.FireRay (transform.position + transform.right * 0.5f, transform.right, play_id);
	}
}
