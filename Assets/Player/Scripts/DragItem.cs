using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	private Image bgImg;
	private Image DragItemImg;
	private Vector3 inputVector;
	private bool drag = false;
	public GameObject Item;
	private int id = 0;
	public TerrainManager Terrain;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		Terrain = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<TerrainManager>();
		bgImg = this.gameObject.GetComponent<Image> ();
		//DragItemImg = transform.GetChild(0).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.touches.Length; i++) {
			if (drag) {
				bgImg.rectTransform.localPosition = Input.touches [i].position - new Vector2(640, 400);
			}
			//Debug.Log (Input.touches [i].position);
		}
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		/*if (drag) {
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
				pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

				inputVector = new Vector3 (pos.x * 2, 0, pos.y * 2);
				//inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

				DragItemImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2), inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
				//Debug.Log (inputVector);
			}
		}*/
	}

	public virtual void OnPointerDown(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2, 0, pos.y * 2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
			if (inputVector.magnitude < 0.9)
				drag = true;
			//Debug.Log (inputVector);
		}
		//OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		GameObject Oneitem = GameObject.Instantiate (Item);
		//Oneitem.transform.parent = gameObject.transform;
		Debug.Log(bgImg.rectTransform.localPosition);
		float x = bgImg.rectTransform.localPosition.x / 640 * 6.5f;
		float z = bgImg.rectTransform.localPosition.y / 400 * 4f;
		Oneitem.transform.position = new Vector3(x, 1f, z);
		Terrain.AddtoItemList (Oneitem);
		Image.Destroy(this.gameObject);
		inputVector = Vector3.zero;
		drag = false;
	}
		
	public void ApplyID(int i){
		id = i;
	}
}
