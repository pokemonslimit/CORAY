using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image bgImg;
	//private Image joystickImg;

	public Vector3 inputDirection{ set; get;}
	public float movetan;
	private float premovetan;
	private float nowtan;
	private int once;
	public bool cantmove;

	void Start () {
		bgImg = this.GetComponent<Image> ();
		once = 0;
	//	joystickImg = transform.GetChild(0).GetComponent<Image> ();
	//	inputDirection = Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputDirection = new Vector3 (pos.x * 2, 0, pos.y * 2);
			if (inputDirection.magnitude > 1) {
				inputDirection = inputDirection.normalized;
			}
			if (inputDirection.magnitude < 0.4)
				cantmove = true;
			else
				cantmove = false;
		
			nowtan = Mathf.Atan2 (inputDirection.z, inputDirection.x) * Mathf.Rad2Deg;
			if (nowtan < 0)
				nowtan += 360f; 
			if (once == 0) {
				movetan = 0;
				once = 1;
			}
			else
				movetan = nowtan - premovetan;
			
			//Debug.Log ("MOVE "+movetan);
			//joystickImg.rectTransform.anchoredPosition = 
			//	new Vector3(inputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3), 
			//		inputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
	}

	public virtual void OnPointerUp(PointerEventData ped){
		once = 0;
		movetan = 0;
	}
	
	// Update is called once per frame
	void Update () {
		premovetan = nowtan;
		//movetan = 0;
	}


}
