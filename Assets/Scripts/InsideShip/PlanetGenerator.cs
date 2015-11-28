using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour
{
    public GameObject[] planets;
    private bool spawned;
    private float timer;

    // Use this for initialization
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                spawned = true;

                GameObject planet = planets[Random.Range(0, planets.Length)];

                planet.GetComponent<PlanetScript>().Activate();

                planet.GetComponent<PlanetScript>().speed = Random.Range(0.4f, 1.0f);

                Vector3 pos = transform.position;
                pos.y += Random.Range(0.0f, 2.0f);
                planet.transform.position = pos; 

                float size = Random.Range(0.2f, 0.6f);
                planet.transform.localScale = new Vector3(size, size, size);
            }
        }
    }

    public void ReclaimPlanet()
    {
        timer = Random.Range(3.0f, 10.0f);
        spawned = false;
    }
}
