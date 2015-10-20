using UnityEngine;
using System.Collections;

public class FuelCanister : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, Input.acceleration.z);

        if (Input.GetKey(KeyCode.A))
        {
            Quaternion temp = transform.rotation;
            temp.z += 0.005f;
            transform.rotation = temp;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion temp = transform.rotation;
            temp.z -= 0.005f;
            transform.rotation = temp;
        }
	}
}
