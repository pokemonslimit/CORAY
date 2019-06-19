using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

	private List<GameObject> NowItem;

	// Use this for initialization
	void Start () {
		NowItem = new List<GameObject> ();
		Debug.Log("First" +NowItem.Count);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddtoItemList(GameObject obj){
		NowItem.Add (obj);
		Debug.Log("After" +NowItem.Count);
	}

	public void DestoryItem(){
		Debug.Log("Destory" +NowItem.Count);
		for (int i = 0; i < NowItem.Count; i++)
			GameObject.Destroy (NowItem [i]);
		NowItem.Clear ();
	}
}
