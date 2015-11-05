using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

	public GameObject text;
	Vector3 temp;
	bool growing;

	// Use this for initialization
	void Start () {
		growing = false;
		text.GetComponent<TextMesh> ().text = "";
		temp = new Vector3 (.3f, 0, 0);
		transform.localScale = temp;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			growing = true;
		}

		if (temp.y >= .3f) {
			growing = false;
			text.GetComponent<TextMesh>().text = "Memes!";
		}

		if (growing) {
			temp += new Vector3(0, Time.deltaTime / 2.0f, 0);
			transform.localScale = temp;
		}
	}
}
