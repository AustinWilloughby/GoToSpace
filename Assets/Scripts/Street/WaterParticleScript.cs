using UnityEngine;
using System.Collections;

public class WaterParticleScript : MonoBehaviour
{
    public GameObject prefab;
    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start()
    {
        
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Vector3 vel = new Vector3(Random.Range(-.9f, .9f), Random.Range(3.0f, 5.0f), 1.0f);
        rigidBody.velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidBody != null)
        {
            if(rigidBody.velocity.y < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -3);
            }
        }
        else
        {
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Fountain")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
