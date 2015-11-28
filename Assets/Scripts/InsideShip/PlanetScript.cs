using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour
{
    private PlanetGenerator generator;
    private bool active;
    public float speed;

    // Use this for initialization
    void Start()
    {
        generator = GameObject.Find("PlanetGenerator").GetComponent<PlanetGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            if (transform.position.x > 10)
            {
                transform.position = generator.gameObject.transform.position;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                generator.ReclaimPlanet();
                active = false;
            }
        }
    }

    public void Activate()
    {
        active = true;
    }
}
