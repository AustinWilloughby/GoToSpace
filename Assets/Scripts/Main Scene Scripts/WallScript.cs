using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{

    public GameObject gameCamera;
    bool fadeZoom = false;
    public float fadeTimer = 1.0f;

    // Use this for initialization
    void Start()
    {
        //A fix for the extremely lazy
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // fade and zoom the camera in when the wall is clicked and the timer is not done
        if (fadeZoom && fadeTimer > 0.0f && gameCamera.transform.position.y <= -.37f)
        {
            // camera zoom
            gameCamera.GetComponent<Camera>().orthographicSize -= .03f;

            // camera drop to adjust for zoom
            Vector3 tempPos = gameCamera.transform.position;
            tempPos.y -= .03f;
            gameCamera.transform.position = tempPos;

            // fade wall
            fadeTimer -= (Time.deltaTime * .5f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer);
        }

        // Move the wall out of the way once it is faded
        if (fadeTimer <= 0.0f)
        {
            gameObject.transform.position = new Vector3(-1000, 0, 0);
        }
    }

    void OnMouseOver()
    {
        // Initiate the fade in when the wall is moused over and clicked
        if (Input.GetMouseButtonDown(0))
        {
            fadeZoom = true;
        }
    }
}
