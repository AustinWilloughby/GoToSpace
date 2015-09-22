using UnityEngine;
using System.Collections;

public class InventoryTest : MonoBehaviour {

	public GameObject guy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Renderer> ().bounds.Intersects (guy.GetComponent<Renderer> ().bounds)) {
			Debug.Log("Guy Intersection!");
		}
	}
}
