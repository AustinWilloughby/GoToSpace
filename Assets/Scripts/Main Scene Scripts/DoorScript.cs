using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public string sceneName;

    private bool doorClicked = false;
    private GameObject guy;

    // Use this for initialization
    void Start()
    {
        guy = GameObject.Find("Guy");
    }

    // Update is called once per frame
    void Update()
    {
        if (doorClicked)
        {
            CheckForSceneTransition();
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            doorClicked = true;
            guy.GetComponent<GuyBehavior>().MakeWalkToPosition(transform.position);
        }
    }

    void CheckForSceneTransition()
    {
        if (Mathf.Abs(transform.position.x - guy.transform.position.x) < .05f)
        {
            guy.GetComponent<GuyBehavior>().enabled = false;
            GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().FadeOut(sceneName);
        }
    }

}
