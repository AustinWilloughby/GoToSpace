
using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
    private GameObject guy;

    // Use this for initialization
    void Start () {
        guy = GameObject.Find("Guy");
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, guy.transform.position) < .5f)
        {
            guy.GetComponent<GuyBehaviorFrogger>().starsLeft--;
            Destroy(gameObject);
        }
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            guy.GetComponent<GuyBehaviorFrogger>().MakeWalkToPosition(transform.position);
        }
    }
}
