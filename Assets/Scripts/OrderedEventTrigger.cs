using UnityEngine;
using System.Collections;

public class OrderedEventTrigger : MonoBehaviour {

	// all of the items in the scene
	public GameObject item01;
	public GameObject item02;
	public GameObject item03;

	private Vector3 screenPoint;
	private Vector3 offset;

	// progress through the order
	int progress;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// If the mouse is clicked and the fade on the wall has been completed
		if (Input.GetMouseButtonDown (0) && GameObject.Find ("Wall").GetComponent<WallScript>().fadeTimer <= 0.0f) 
		{
			// If an item is clicked on, check the progress. If it matches with its position then move progress forward otherwise reset it
			if (item01.GetComponent<ItemScript>().mousedOver)
			{
				if (progress == 0)
					progress += 1;
				else { progress = 0; }

				Debug.Log(progress);
                Debug.Log("!");
			}
			if (item02.GetComponent<ItemScript>().mousedOver)
			{
				if (progress == 1)
					progress += 1;
				else { progress = 0; }

				Debug.Log(progress);
			}
			if (item03.GetComponent<ItemScript>().mousedOver)
			{
				if (progress == 2)
					progress += 1;
				else { progress = 0; }

				Debug.Log(progress);
			}
		}	
	}
}
