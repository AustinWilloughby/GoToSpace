using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public GameObject gameCamera;
	bool fadeZoom = false;
	public float fadeTimer = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// fade and zoom the camera in when the wall is clicked
		if (fadeZoom && fadeTimer > 0.0f) {
			// camera zoom
			gameCamera.GetComponent<Camera>().orthographicSize -= .03f;

			// camera drop to adjust for zoom
			Vector3 tempPos = gameCamera.transform.position;
			tempPos.y -= .03f;
			gameCamera.transform.position = tempPos;

			// fade wall
			fadeTimer -= (Time.deltaTime * .5f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer);
		}
	}

	void OnMouseOver() {
		// Initiate the fade in when the wall is moused over and clicked
		if (Input.GetMouseButtonDown (0)) {
			fadeZoom = true;
		}
	}
}
