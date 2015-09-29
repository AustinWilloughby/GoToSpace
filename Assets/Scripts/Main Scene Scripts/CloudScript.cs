using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If fade in has not completed
        if (GameObject.Find("Wall").GetComponent<WallScript>().fadeTimer > 0.0f)
        {
            // move right
            Vector2 tempPos = transform.position;
            tempPos.x += .009f;
            transform.position = tempPos;

            // once the cloud is off screen
            if (transform.position.x > 13)
            {
                // 1 in 1000 chance every frame that it will reset to the left
                if (Random.Range(0, 1000) == 1)
                {
                    tempPos.x = -13.0f;
                    transform.position = tempPos;
                }
            }
        }
    }
}