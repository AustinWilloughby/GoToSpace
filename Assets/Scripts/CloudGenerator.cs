using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public int cloudFrequency;

    public int numStartingClouds;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numStartingClouds; i++)
        {
            Vector3 randPos = transform.position;
            randPos.x += Random.Range(5f, 25f);
            randPos.y += Random.Range(0f, 25.0f);
            randPos.z += Random.Range(0f, 5.0f);

            GameObject cloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)];

            float randScale = Random.Range(.2f, .7f);
            cloud.transform.localScale = new Vector3(randScale, randScale, 1);

            cloud.GetComponent<CloudScript>().speed = Random.Range(1f, 2f);

            GameObject.Instantiate(cloud, randPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, cloudFrequency) == 0)
        {
            Vector3 randPos = transform.position;
            randPos.y += Random.Range(0f, 15.0f);
            randPos.z += Random.Range(0f, 5.0f);

            GameObject cloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)];

            float randScale = Random.Range(.2f, .5f);
            cloud.transform.localScale = new Vector3(randScale, randScale, 1);

            cloud.GetComponent<CloudScript>().speed = Random.Range(1f, 2f);

            GameObject.Instantiate(cloud, randPos, Quaternion.identity);
        }
    }
}
