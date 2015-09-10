using UnityEngine;
using System.Collections;

public class IntroTransition : MonoBehaviour {

    float timer = 1.0f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 temp = transform.position;
        if (transform.position.y > -.37f)
        {
            temp.y -= .04f;
            transform.position = temp;
        }
	}
}
