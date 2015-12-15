using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	public GameObject lastTouch;
	public List<GameObject> ammo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		TurnToFace();
	}

	private void TurnToFace()
	{
		Vector3 direction = lastTouch.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90.0f, Vector3.forward);
	}

	public void Fire(){
		if(ammo.Count > 0)
		{
			Vector3 direction = lastTouch.GetComponent<TouchTrack>().targLocation - transform.position;
			ammo[0].GetComponent<ProjectileScript>().FireProjectile((Vector2)direction);
			ammo.Remove(ammo[0]);
		}
	}
	
}
