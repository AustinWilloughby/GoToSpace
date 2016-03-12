using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	private GameObject[] asteroids;
	private float spawnTimer;
	public GameObject ship;

	// Use this for initialization
	void Start () {
		spawnTimer = 0;
		ship = GameObject.Find ("ship");
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer <= 0) {
			spawnTimer = Random.Range (.3f, .6f);
			GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];

			Vector2 position = Random.insideUnitCircle * 12;
			Vector3 startPos = (Vector3)position + ship.transform.position;

			GameObject.Instantiate(asteroid, startPos, Quaternion.identity);

		} 

		else {
			spawnTimer -= Time.deltaTime;
		}
	}
}
