using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayRefractionController : RayInteractionController {

	public float externalN = 1f;
	public float internalN = 0.5f;

	public override bool DoRayAction(RaySegmentController segment, RaycastHit hitInfo, Vector3 inDirection){

		// create ray based on normal
		Vector3 outDirection = inDirection;

		// compute incoming angle theta1
		float theta1 = Vector3.Angle(inDirection  * -1, hitInfo.normal) * Mathf.Deg2Rad;


		float nRatio;
		nRatio = (externalN / internalN);

		if (nRatio * Mathf.Sin(theta1) >= 1f) {
			// total internal reflection
			outDirection = Vector3.Reflect (inDirection, hitInfo.normal);
		} else {
			// refraction
			float c = Vector3.Dot (hitInfo.normal, inDirection) * -1;
			outDirection = nRatio * inDirection + ((nRatio*c) - Mathf.Sqrt(1 - (nRatio*nRatio)*(1 - c*c)) ) * hitInfo.normal;
		}
		segment.SpawnChildRay (hitInfo.point, outDirection);
		return true;
	}
}
