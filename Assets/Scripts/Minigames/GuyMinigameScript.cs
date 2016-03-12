using UnityEngine;
using System.Collections;

public class GuyMinigameScript : MonoBehaviour {

    float startY;
    float endY;

    public float difference = 2.0f;

    public bool done;

	// Use this for initialization
	void Start () {
        startY = gameObject.transform.position.y;
        endY = startY + difference;
        done = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < endY && !done)
        {
            Vector3 temp = transform.position + new Vector3(0, 0.5f, 0);
            transform.position = temp;
        }

        if (done && transform.position.y > startY)
        {
            Vector3 temp = transform.position - new Vector3(0, 0.5f, 0);
            transform.position = temp;
        }
	}
}
