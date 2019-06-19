using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackController : MonoBehaviour, IPointerDownHandler {

	public GameObject Player;
	private Image buttonImg;
	public Vector3 inputDirection{ set; get;}
	// Use this for initialization
	void Start () {
		buttonImg = this.GetComponent<Image> ();
	}

	public virtual void OnPointerDown(PointerEventData ped){
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(buttonImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / buttonImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / buttonImg.rectTransform.sizeDelta.y);

			inputDirection = new Vector3 (pos.x * 2, 0, pos.y * 2);
			if (inputDirection.magnitude < 0.9) {
				Player.SendMessage ("Attack", 0.5f);
			}
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
