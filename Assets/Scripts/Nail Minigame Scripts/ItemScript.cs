using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public bool mousedOver;

	// Use this for initialization
	void Start () {
		mousedOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		mousedOver = true;
	}

	void OnMouseExit(){
		mousedOver = false;
	}
}
