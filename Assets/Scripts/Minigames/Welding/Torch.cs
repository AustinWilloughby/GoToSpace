using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {

    float sparkTimer = 0.1f;
    public GameObject spark;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = gameObject.GetComponent<AudioSource>();
        try
        {
            source.volume = GameObject.Find("AmbientSounds").GetComponent<AmbienceSingleton>().volume;
        }
        catch { }
        source.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 3.0f;
        gameObject.transform.position = mousePos;

        if (Input.GetMouseButton(0))
        {
            GenerateSparks();
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            source.Stop();
        }
	}

    void GenerateSparks()
    {
        sparkTimer -= Time.deltaTime;
        if (sparkTimer <= 0)
        {
            sparkTimer = 0.1f;
            Instantiate(spark, transform.position, Quaternion.identity);
        }
    }
}
