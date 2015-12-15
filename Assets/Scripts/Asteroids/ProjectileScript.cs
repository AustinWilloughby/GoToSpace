using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	private Vector2 velocity = new Vector2(0,0);
	private bool active = false;
	public Ship spaceship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (spaceship.gameObject.transform.position, transform.position) > 15) {
			velocity = new Vector2 (0, 0);
			active = false;
			Vector3 resetPos = spaceship.gameObject.transform.position;
			resetPos.z += 1;
			transform.position = resetPos;
			spaceship.ammo.Add(gameObject);
		} 

		else {
			Vector3 tempPos = transform.position;
			tempPos += (Vector3)velocity * Time.deltaTime;
			transform.position = tempPos;
		}
	}


	public void FireProjectile(Vector2 dir)
	{
		velocity = dir.normalized * 8;
		active = true;
	}
}
