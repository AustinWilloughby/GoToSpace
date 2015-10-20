using UnityEngine;
using System.Collections;

public class FuelTankScript : MonoBehaviour {

    public Sprite deathSprite;
    public int intakeCount;
    GameObject deathScreen;

	// Use this for initialization
	void Start () {
        intakeCount = 0;
        deathScreen = GameObject.Find("ded");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "fuel" && !coll.gameObject.GetComponent<FuelScript>().hasCollided)
        {
            coll.gameObject.GetComponent<FuelScript>().hasCollided = true;
            intakeCount += 1;
            if (intakeCount > 15)
            {
                deathScreen.GetComponent<SpriteRenderer>().sprite = deathSprite;
            }
            coll.transform.gameObject.GetComponent<FuelScript>().destroyTrigger = true;
        }
    }
}
