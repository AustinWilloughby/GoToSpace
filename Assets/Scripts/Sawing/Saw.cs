using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 3.0f;
        gameObject.transform.position = mousePos;
	}
}
