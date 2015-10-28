using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 vel = new Vector3(Random.Range(-2.0f, 2.0f), 3.0f, 0.0f);
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
