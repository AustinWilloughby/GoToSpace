using UnityEngine;
using System.Collections;

public class WeldTarget : MonoBehaviour {

    public float darkness;
	public bool completed;
	public bool failed;
	GameObject torchPoint;

	// Use this for initialization
	void Start () {
        darkness = 1.0f;
		torchPoint = GameObject.Find ("TorchPoint");
		completed = false;
		failed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (torchPoint.transform.position, transform.position) < 0.7f && Input.GetMouseButton(0)) {
			darkness -= .01f;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (darkness, darkness, darkness, 1.0f);
		}

		if (!completed && !failed && darkness <= 0.5f) 
		{
			failed = false;
			completed = true;
		}

		if (!failed && darkness <= 0.0f) 
		{
			completed = false;
			failed = true;
		}
	}
}
