using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetupManage : MonoBehaviour, IPointerDownHandler {
	private int stage = 0;
	public Camera maincamera;
	private Image[] UI_Img;
	private Text[] UI_Text;
	private Text Check;
	public GameObject player1;
	public EnemySpawnerControl spawner;
	public Text HP;
	public Text Title;
	private int childcount;
	private float CountDown = 3f;
	private float CurrentCount;
	private List<int> BackbagItem;
	public List<Image> ItemCandidate;
	public TerrainManager Terrain;
	public TowerController Tower;


	// Use this for initialization
	void Start () {
		childcount = transform.childCount;
		UI_Img = new Image[childcount];
		UI_Text = new Text[childcount];
		BackbagItem = new List<int> ();
		BackbagItem.Add(0);
		//BackbagItem.Add(2);
		CreatItem ();
		for (int i = 0; i < childcount; i++) {
			if (transform.GetChild (i).GetComponent<Image> ())
				UI_Img [i] = transform.GetChild (i).GetComponent<Image> ();
			//Debug.Log (i);
			else if (transform.GetChild (i).GetComponent<Text> ())
				UI_Text [i] = transform.GetChild (i).GetComponent<Text> ();
		}
		//Check = transform.GetChild(6).GetComponent<Text> ();
		check_box (false);
		HP.enabled = false;
		Title.enabled = false;
		UI_Img [7].enabled = false;
	}

	public virtual void OnPointerDown(PointerEventData ped){
		//Check to Start
		switch (stage) {
		case 0:
			if (PushButton(ped, UI_Img[1], 0.9f))
				stage = 1;
			if(PushButton(ped, UI_Img[2], 0.9f)){
				CreatItem ();
			}
			break;
		case 1:
			if (PushButton (ped, UI_Img [4], 0.9f)) {
				stage = 2;
				CurrentCount = CountDown;
			}
			if (PushButton (ped, UI_Img [5], 0.9f)) {
				stage = 0;
			}
			break;
		case 2://Count Down
			break;
		case 3://Game Start
			break;
		case 4:
			if (PushButton (ped, UI_Img [7], 20f)) {
				stage = 5;
			}
			break;
		case 5://Restart
			if (PushButton (ped, UI_Img [4], 0.9f)) {
				if (BackbagItem.Count == 0) {
					stage = 2;
					Tower.SendMessage ("ResetHP");
					CurrentCount = CountDown;
				}
				else
					stage = 0;
			}
			if (PushButton (ped, UI_Img [5], 0.9f)) {
				stage = 5;// Shop
			}
			break;
			
		}
	}

	// Update is called once per frame
	void Update () {
		switch (stage) {
		case 0:
			check_box (false);
			backbag (true);
			break;
		case 1:
			UI_Text [6].text = "Are you sure to Start?";
			check_box (true);
			backbag (false);
			break;
		case 2: // Count
			check_box (false);
			backbag (false);
			Count ();
			break;
		case 3:
			break;
		case 4:
			Title.enabled = true;
			Title.text = "Game Over";
			UI_Img [7].enabled = true;
			spawner.SendMessage ("DestoryAll");
			break;
		case 5:
			Title.enabled = false;
			UI_Img [7].enabled = false;
			UI_Text [6].text = "Do you want to Restart?";
			check_box (true);
			break;
		}
		//Debug.Log (stage);
	}

	private void check_box(bool open){
		for (int i = 3; i < 6; i++) {
			if (open) {
				UI_Img[i].enabled = true;
				UI_Text [6].enabled = true;
			} else {
				UI_Img[i].enabled = false;
				UI_Text [6].enabled = false;
			}
		}
	}

	private void backbag(bool open){
		for (int i = 0; i < 3; i++) {
			if (open) {
				//Check.enabled = true;
				UI_Img[i].enabled  = true;
			} else {
				UI_Img[i].enabled = false;
				//Check.enabled = false;
			}
		}
	}

	private void CreatItem(){
		Terrain.DestoryItem ();
		for (int i = 0; i < BackbagItem.Count; i++) {
			Image item = Image.Instantiate (ItemCandidate [BackbagItem [i]]);
			item.transform.parent = gameObject.transform;
			item.rectTransform.localPosition = Vector3.zero;
			//item.GetComponent<DragItem> ().ApplyID (BackbagItem [i]);
			//item.rectTransform.position = gameObject.transform.position + new Vector3(-65f, -73.3f, 0f);
			//item.rectTransform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}
	}

	private bool PushButton(PointerEventData ped, Image Img, float length){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(Img.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / Img.rectTransform.sizeDelta.x);
			pos.y = (pos.y / Img.rectTransform.sizeDelta.y);
		}
		Vector3 inputVector = new Vector3 (pos.x * 2, 0, pos.y * 2);
		//Debug.Log (inputVector);
		if (inputVector.magnitude < length)
			return true;
		else
			return false;
	}

	private void GameOver(){
		stage = 4;
	}

	private void Count(){
		Title.enabled = true;
		Title.text = "3";
		CurrentCount -= Time.deltaTime;
		if (CurrentCount <= 2.0f)
			Title.text = "2";
		if (CurrentCount <= 1.0f)
			Title.text = "1";
		if (CurrentCount <= 0.0f)
			Title.text = "Start";
		if (CurrentCount <= -1f) {
			spawner.SendMessage ("GameStart", true);
			Title.enabled = false;
			HP.enabled = true;
			stage = 3;
		}
	}
	public int GetStage(){
		return stage;
	}


}
