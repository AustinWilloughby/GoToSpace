using UnityEngine;
using System.Collections;

public class RocketThruster : MonoBehaviour
{
    public GameObject particle;
    public float particleInterval;
    private float particleTimer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        particleTimer -= Time.deltaTime;
        if (particleTimer <= 0)
        {
            particleTimer = particleInterval;
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
