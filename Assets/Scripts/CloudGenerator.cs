using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public int cloudFrequency;

    public int numStartingClouds;
    public float sizeMultiplier = 1;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numStartingClouds; i++)
        {
            Vector3 randPos = transform.position;
            randPos.x += Random.Range(5f * sizeMultiplier, 25f * sizeMultiplier);
            randPos.y += Random.Range(0f, 25.0f * sizeMultiplier);
            randPos.z += Random.Range(0f, 5.0f * sizeMultiplier);

            GameObject cloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)];

            float randScale = Random.Range(.2f * sizeMultiplier, .7f * sizeMultiplier);
            cloud.transform.localScale = new Vector3(randScale, randScale, 1);

            if (sizeMultiplier > .9f)
            {
                cloud.GetComponent<CloudScript>().speed = Random.Range(1f * sizeMultiplier, 2f * sizeMultiplier);
                cloud.GetComponent<CloudScript>().scale = 1;
            }
            else
            {
                cloud.GetComponent<CloudScript>().speed = Random.Range(.8f * sizeMultiplier, 1.6f * sizeMultiplier);
                cloud.GetComponent<CloudScript>().scale = sizeMultiplier * 1.5f;
            }

            GameObject.Instantiate(cloud, randPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, cloudFrequency) == 0)
        {
            Vector3 randPos = transform.position;
            randPos.y += Random.Range(0f, 15.0f * sizeMultiplier);
            randPos.z += Random.Range(0f, 5.0f * sizeMultiplier);

            GameObject cloud = cloudPrefabs[Random.Range(0, cloudPrefabs.Length - 1)];

            float randScale = Random.Range(.2f * sizeMultiplier, .5f * sizeMultiplier);
            cloud.transform.localScale = new Vector3(randScale, randScale, 1);


            if (sizeMultiplier > .9f)
            {
                cloud.GetComponent<CloudScript>().speed = Random.Range(1f * sizeMultiplier, 2f * sizeMultiplier);
                cloud.GetComponent<CloudScript>().scale = 1;
            }
            else
            {
                cloud.GetComponent<CloudScript>().speed = Random.Range(.8f * sizeMultiplier, 1.6f * sizeMultiplier);
                cloud.GetComponent<CloudScript>().scale = sizeMultiplier * 2.1f;
            }

            GameObject.Instantiate(cloud, randPos, Quaternion.identity);
        }
    }
}
