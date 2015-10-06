using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
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
        if (Vector2.Distance((Vector2)guy.transform.position, (Vector2)transform.position) < .13f)
        {
            Application.LoadLevel("StreetScene");
        }
    }

}
