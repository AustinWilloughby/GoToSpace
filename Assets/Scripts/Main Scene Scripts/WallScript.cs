using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{

    public GameObject gameCamera;
    bool fadeZoom = false;
    public float fadeTimer = 5.0f;

    // Use this for initialization
    void Start()
    {
        //A fix for the extremely lazy
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // fade and zoom the camera in when the wall is clicked and the timer is not done
        if (fadeZoom && fadeTimer > 0.0f && gameCamera.transform.position.y <= 4.63f)
        {
            // camera zoom
            gameCamera.GetComponent<Camera>().orthographicSize -= .07f;

            // camera drop to adjust for zoom
            Vector3 tempPos = gameCamera.transform.position;
            tempPos.y -= .0425f;
            tempPos.x -= .0057f;
            gameCamera.transform.position = tempPos;

            // fade wall
            fadeTimer -= (Time.deltaTime * .5f);
        }

        if (fadeTimer <= 0)
        {
            Application.LoadLevel("Workshop");
        }
    }

    void OnMouseOver()
    {
        // Initiate the fade in when the wall is moused over and clicked
        if (Input.GetMouseButtonDown(0))
        {
            fadeZoom = true;
            if (MusicSingleton.Instance != null)
            {
                MusicSingleton.Instance.FadeOut(2.4f);
            }
        }
    }
}
