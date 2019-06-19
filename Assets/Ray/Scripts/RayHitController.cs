using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitController : MonoBehaviour {

	public HashSet<RaySegmentController> hitRays;

	// Use this for initialization
	void Awake () {
		hitRays = new HashSet<RaySegmentController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hitRays.Count > 0) {
			SendMessage ("HitByRays", hitRays, SendMessageOptions.RequireReceiver);
			hitRays.Clear ();
		}
	}

	public bool CanHitByRay(RaySegmentController ray){
		return !hitRays.Contains (ray.primary);
	}

	public void AddHitRay(RaySegmentController ray){
		hitRays.Add (ray);
	}
}
