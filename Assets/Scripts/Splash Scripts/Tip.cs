using UnityEngine;
using System.Collections;

public class Tip : MonoBehaviour {

    public bool doTip = false;
    float tipTimer = 0.1f;
    float hangTimer = 0.03f;
    float dir = 1.0f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (doTip)
        {
            if (tipTimer >= 0.0f)
            {
                tipTimer -= Time.deltaTime * 0.5f;
                gameObject.transform.Rotate(new Vector3(0, 0, dir));
            }
            else if (hangTimer > 0.0f)
            {
                hangTimer -= Time.deltaTime * 0.5f;
            }
            else if (dir > 0.0f)
            {
                tipTimer = .1f;
                dir = -1.0f;
            }
        }
	}
}
