using UnityEngine;
using System.Collections;

public class FuelScript : MonoBehaviour {

    float lifeSpan = 0.5f;
    public bool destroyTrigger;
    public bool hasCollided;

	// Use this for initialization
	void Start () {
        destroyTrigger = false;
        hasCollided = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (destroyTrigger)
        {
            lifeSpan -= Time.deltaTime;
        }

        if (lifeSpan <= 0)
        {
            GameObject.Find("FuelTankCollider").GetComponent<FuelTankScript>().intakeCount -= 1;
            Destroy(gameObject);
        }
	}
}
