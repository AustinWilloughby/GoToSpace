using UnityEngine;
using System.Collections;

public class SawPoint : MonoBehaviour {

    bool outOfBounds;
    bool onScratch;
    public GameObject scratch;
    public float outOfBoundsTime;

	// Use this for initialization
	void Start () {
        onScratch = false;
        outOfBounds = false;
        outOfBoundsTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (outOfBounds)
        {
            outOfBoundsTime += Time.deltaTime;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter");

        if (other.tag == "Wall")
        {
            outOfBounds = true;
        }

        if (other.tag == "Scratch")
        {
            onScratch = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            if (!onScratch)
            {
                Vector3 temp = transform.position;
                temp += new Vector3(0, 0, 1.0f);
                Instantiate(scratch, temp, Quaternion.identity);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            outOfBounds = false;
        }

        if (other.tag == "Scratch")
        {
            onScratch = false;
        }
    }
}
