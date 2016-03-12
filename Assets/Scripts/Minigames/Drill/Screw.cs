using UnityEngine;
using System.Collections;

public class Screw : MonoBehaviour {

	bool mousedOver = false;
	public bool complete = false;
	Drill drillScript;

	// Use this for initialization
	void Start () {
		drillScript = GameObject.Find("DrillPoint").GetComponent<Drill>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) && mousedOver) {
			if (complete){
				drillScript.overDrillTime += Time.deltaTime;
			} 
			else 
			{
				transform.position += new Vector3(.1f, 0, 0);
			}
		}
	}

	void OnMouseEnter(){
		mousedOver = true;
	}

	void OnMouseExit(){
		mousedOver = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Wall") {
			complete = true;
		}
	}
}
