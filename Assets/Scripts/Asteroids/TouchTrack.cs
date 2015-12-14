using UnityEngine;
using System.Collections;

public class TouchTrack : MonoBehaviour {

	public Vector3 targLocation;
	private bool fired = true;
	public Ship spaceShip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x != targLocation.x || transform.position.y != targLocation.y) {
			Vector3 moveTo = targLocation - transform.position;
			moveTo *= Time.deltaTime * 5;
			moveTo += transform.position;
			transform.position = moveTo;

			if(Mathf.Abs(transform.position.x - targLocation.x) < .5f && Mathf.Abs(transform.position.y - targLocation.y) < .5f)
			{
				if(fired == false)
				{
					fired = true;
					spaceShip.Fire();
				}
				else{
					if (Input.GetMouseButtonDown (0)) {
						Vector2 mousePos = Input.mousePosition;
						targLocation = Camera.main.ScreenToWorldPoint(mousePos);
						fired = false;
					}
				}
			}
		}
	}
}
