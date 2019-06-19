using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScaterringController : RayInteractionController {

	public float scatterRatio = 1.0f;


	public override bool DoRayAction(RaySegmentController segment, RaycastHit hitInfo, Vector3 inDirection){

		Vector3 inverseNormal = hitInfo.normal * -1;
		Vector3 right = new Vector3(inverseNormal.z, inverseNormal.y, -inverseNormal.x);
		Vector3 outDirection1 = inDirection + right * scatterRatio;
		Vector3 outDirection2 = inDirection - right * scatterRatio;

		if (outDirection1.sqrMagnitude > 0) {
			segment.SpawnChildRay (hitInfo.point, outDirection1.normalized);
		}
		if (outDirection2.sqrMagnitude > 0) {
			segment.SpawnChildRay (hitInfo.point, outDirection2.normalized);
		}
		return true;
	}
}
