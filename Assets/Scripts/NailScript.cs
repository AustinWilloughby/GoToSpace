using UnityEngine;
using System.Collections;

public class NailScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponentsInChildren<ItemScript>()[1].mousedOver)
        {
            Debug.Log("Correct hit!");
        }

        else if (Input.GetMouseButtonDown(0) && GetComponent<ItemScript>().mousedOver)
        {
            Debug.Log("Missed hit!");
        }
	}
}
