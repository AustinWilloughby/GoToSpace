using UnityEngine;
using System.Collections;

public class Drill : MonoBehaviour {

    public float intensity = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Shake(intensity);
        }
	}

    void Shake(float intensity)
    {
        Vector3 temp = new Vector3(Mathf.Sin(Time.time * intensity), transform.position.y, transform.position.z);
        transform.position = temp;
    }
}
