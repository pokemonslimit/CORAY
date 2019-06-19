using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayReflectController : RayInteractionController {

	public override bool DoRayAction(RaySegmentController segment, RaycastHit hitInfo, Vector3 inDirection){
		// create ray based on normal
		Vector3 outDirection = Vector3.Reflect (inDirection, hitInfo.normal);
		segment.SpawnChildRay (hitInfo.point, outDirection);
		return true;
	}
}
