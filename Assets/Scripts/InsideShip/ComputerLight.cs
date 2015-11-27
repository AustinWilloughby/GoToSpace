using UnityEngine;
using System.Collections;

public class ComputerLight : MonoBehaviour
{
    public float minChange;
    public float maxChange;

    private float timer;
    public bool shown;


    // Use this for initialization
    void Start()
    {
        minChange = Random.Range(0.6f, 1.0f);
        maxChange = minChange + Random.Range(0.3f, 2.0f);
        timer = Random.Range(minChange, maxChange);
        if (Random.Range(0, 2) == 0)
        {
            shown = true;
        }
        else
        {
            shown = false;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = shown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            shown = !shown;
            gameObject.GetComponent<SpriteRenderer>().enabled = shown;

            timer = Random.Range(minChange, maxChange);
        }
    }
}
