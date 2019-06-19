using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour {

	private List<GameObject> NowItem = new List<GameObject>();
	public int Maxhp = 100;
	private int currentHp;
	public Text PlayerHPText;
	public EnemySpawnerControl spawner;
	public SetupManage Setup;

	// Use this for initialization
	void Start () {
		currentHp = Maxhp;
		UpdateHpText ();
		//NowItem = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddtoItemList(GameObject obj){
		NowItem.Add (obj);
		//Debug.Log("After" +NowItem.Count);
	}

	public void DestoryItem(){
		//Debug.Log("Destory" +NowItem.Count);
		for (int i = 0; i < NowItem.Count; i++)
			//GameObject.Destroy (NowItem [i]);
		NowItem.Clear ();
	}

	void OnEnemyTouch(){
		currentHp -= 1;
		UpdateHpText ();
		int stage = Setup.GetStage();
		if (currentHp <= 0 && stage == 3) {
			//PlayerHPText.text = "Game Over";
			//PlayerHPText.fontSize = 80;
			PlayerHPText.enabled = false;
			spawner.SendMessage ("GameStart", false);
			Setup.SendMessage ("GameOver");
		}
	}

	void UpdateHpText(){
		PlayerHPText.text = "HP : " + currentHp;
	}

	void ResetHP(){
		currentHp = Maxhp;
		PlayerHPText.text = "HP : " + currentHp;
	}
}
