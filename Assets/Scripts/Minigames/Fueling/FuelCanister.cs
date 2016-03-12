using UnityEngine;
using System.Collections;

public class FuelCanister : MonoBehaviour {

	Gyroscope myGyro;

	// Use this for initialization
	void Start () {
		myGyro = Input.gyro;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("SpeechBubble").GetComponent<MiniGameTutorial> ().done) {
			transform.Rotate (0, 0, myGyro.userAcceleration.x);
		
			if (Input.GetKey (KeyCode.A)) {
				Quaternion temp = transform.rotation;
				temp.z += 0.005f;
				transform.rotation = temp;
			}
		
			if (Input.GetKey (KeyCode.D)) {
				Quaternion temp = transform.rotation;
				temp.z -= 0.005f;
				transform.rotation = temp;
			}
		}
	}
}