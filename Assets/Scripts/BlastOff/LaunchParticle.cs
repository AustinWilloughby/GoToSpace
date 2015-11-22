using UnityEngine;
using System.Collections;

public class LaunchParticle : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float lifeTimer;
    // Use this for initialization
    void Start()
    {
        Color particleColor = GetComponent<SpriteRenderer>().color;
        particleColor.g = Random.Range(70, 256);
        GetComponent<SpriteRenderer>().color = particleColor;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Vector3 vel = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-0.5f, -1.0f), 0);
        rigidBody.velocity = vel;
        lifeTimer = Random.Range(.5f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
