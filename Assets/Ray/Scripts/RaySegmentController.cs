using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySegmentController : MonoBehaviour {

	private LineRenderer lineRenderer;

	public List<RaySegmentController> children;
	public LayerMask[] validLayers;
	public float maxReflectEnergy = 1;
	public float currentReflectEnergy;

	public float rayLifeTime = 1.0f;
	public float reflectEspilon = 0.1f;

	public int maxAdvanceLimit = 100;

	public int layerMaskValue;

	public RaySegmentController primary;
	public RaySegmentController parent;
	public int ray_id;

	public GameObject enemyColliderObject;

	public Material[] materialsById;

	// Use this for initialization
	void Awake () {
		lineRenderer = GetComponent<LineRenderer> ();
		children = new List<RaySegmentController> ();
		layerMaskValue = 0;
		foreach(var mask in validLayers){
			layerMaskValue |= mask.value;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateColliderShape(Vector3 startPoint, Vector3 endPoint){
		// change rotation
		enemyColliderObject.transform.rotation = Quaternion.LookRotation (endPoint - startPoint, Vector3.up);
		// move to center point
		enemyColliderObject.transform.position = (endPoint + startPoint) / 2;
		// scale z
		Vector3 oldScale = enemyColliderObject.transform.localScale;
		oldScale.z = (endPoint - startPoint).magnitude;
		enemyColliderObject.transform.localScale = oldScale;
		enemyColliderObject.GetComponent<Renderer> ().material = materialsById [ray_id];

	}


	public void InitPrimaryRay(Vector3 startPoint, Vector3 direction, int id){
		// Init some stuff
		ray_id = id;
		this.parent = null;
		this.primary = this;
		currentReflectEnergy = maxReflectEnergy;
		// change color
		// Set up self-kill
		Invoke ("KillYourself", rayLifeTime);
		// Compute ray segment destination
		Vector3 endPoint;
		FindEndPoint(startPoint, direction.normalized, out endPoint);
		// Draw the line
		DrawLine(startPoint, endPoint);
	}


	public void InitSecondaryRay(RaySegmentController parent, Vector3 startPoint, Vector3 direction){
		// init some stuff
		this.parent = parent;
		this.primary = parent.primary;
		// Compute ray segment destination
		Vector3 endPoint;
		FindEndPoint(startPoint, direction, out endPoint);
		// Draw the line
		DrawLine(startPoint, endPoint);
	}

	void DrawLine(Vector3 startPoint, Vector3 endPoint){
		lineRenderer.SetPosition (0, startPoint);
		lineRenderer.SetPosition (1, endPoint);		
		UpdateColliderShape (startPoint, endPoint);
	}

	bool FindNextPoint(Vector3 position, Vector3 direction, out Vector3 lastPoint){
		bool terminate = false;
		RaycastHit hitInfo;
		bool hit = Physics.Raycast (position, direction, out hitInfo, Mathf.Infinity, layerMaskValue);
		if (hit) {
			// handle interaction
			var interactionController = hitInfo.collider.gameObject.GetComponent<RayInteractionController>();
			if (interactionController != null) {
				// cost energy
				currentReflectEnergy -= interactionController.energyCost;
				if (currentReflectEnergy > 0.0f) {
					terminate = interactionController.DoRayAction (this, hitInfo, direction);

				} else {
					// No energy, terminate
					terminate = true;
				}

			}
			lastPoint = (Vector3)hitInfo.point;

		} else {
			// TODO: handle miss cases
			// However, this should not happen!
			Debug.LogError("There is a missing ray from: " + position + ", dir: " + direction);
			lastPoint = position + direction * 30;
			terminate = true;
		}
		return terminate;
	}


	public void SpawnChildRay(Vector3 position , Vector3 direction){
		GameObject rayObject = Instantiate (gameObject);
		rayObject.transform.parent = gameObject.transform.parent;
		var rayController = rayObject.GetComponent<RaySegmentController> ();
		rayController.InitSecondaryRay (this, position + direction * reflectEspilon, direction );
		children.Add (rayController);
	}

	void FindEndPoint(Vector3 position, Vector3 direction, out Vector3 lastPoint){
		Vector3 currentEndPoint;
		int count = maxAdvanceLimit;
		while (!FindNextPoint (position, direction, out currentEndPoint)) {
			position = currentEndPoint + direction * reflectEspilon;
			count--;
			if (count == 0) {
				Debug.LogWarning ("Ray reach max bounce limit!");
				break;
			}
		}
		lastPoint = currentEndPoint;
	}

	void KillYourself(){
		foreach (var child in children) {
			child.KillYourself ();
		}
		Destroy (gameObject);
	}

	public void OnHitTarget(RayHitController hitController){
		if (hitController.CanHitByRay (this.primary)) {
			hitController.AddHitRay (this.primary);
		}
		// this object cannot be hit, just ignore
	}


}
