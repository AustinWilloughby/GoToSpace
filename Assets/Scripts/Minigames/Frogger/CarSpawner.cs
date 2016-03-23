using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

    public GameObject car;
    public bool carsMoveRight = true;
    public float spawnTimer = 2.0f;
    public float timeToSpawn = 6.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnTimer > 0.0f)
        {
            spawnTimer -= Time.deltaTime;
        }
        if (spawnTimer <= 0.0f)
        {
            spawnCar();
            spawnTimer = timeToSpawn;
            timeToSpawn += Random.Range(-1.0f, 1.0f);
        }
    }

    void spawnCar()
    {
        GameObject c = GameObject.Instantiate(car);
        c.transform.position = transform.position;
        if (carsMoveRight)
        {
            c.GetComponent<Car>().movingRight = true;
        }
        else
        {
            c.GetComponent<Car>().movingRight = false;
        }
    }
}
