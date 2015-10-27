using UnityEngine;
using System.Collections;

public class WeldTarget : MonoBehaviour {

    public float darkness;

	// Use this for initialization
	void Start () {
        darkness = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Anything");
        if (coll.gameObject.tag == "WeldTarget" && Input.GetMouseButton(0))
        {
            Destroy(coll.gameObject);
            darkness -= .1f;
            coll.gameObject.GetComponent<SpriteRenderer>().color = new Color(darkness, darkness, darkness, 1.0f);
        }
    }
}
