using UnityEngine;
using System.Collections;

public class MiniGameTutorial : MonoBehaviour {

    public GameObject text;
    Vector3 temp;
    public bool growing;
    bool shrinking;
	public bool done;
    float waiting;

    // Use this for initialization
    void Start()
    {
        growing = true;
        shrinking = false;
        text.GetComponent<TextMesh>().text = "";
        temp = new Vector3(.3f, 0, 0.1f);
        transform.localScale = temp;
		done = false;
        waiting = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting > 0.0f)
        {
            waiting -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                shrinking = true;
				done = true;
            }
        }

        if (temp.y >= .3f)
        {
            growing = false;

            switch (Application.loadedLevelName)
            {
                case "Nail":
                    text.GetComponent<TextMesh>().text = "Tap the head of the nail to build\nthe platform. Careful not to miss\nor you might damage the nail.";
                    break;
                case "Weld":
                    text.GetComponent<TextMesh>().text = "Weld the four grey lines. You can\ntell they're done when they are\ndark, but don't over weld them.";
                    break;
                case "Drill":
                    text.GetComponent<TextMesh>().text = "Drill the screws by holding down\non the screw head until they are in.\nDon't drill more than necessary.";
                    break;
                case "JetFuel":
                    text.GetComponent<TextMesh>().text = "Tilt your device to pour the fuel.\nDon't pour it to fast or it might\nexplode!";
                    break;
                case "Sawing":
                    text.GetComponent<TextMesh>().text = "Saw back and forth between the gap.\nStay in line or you might cause\nscratches and ruin the metal.";
                    break;
                default:
                    text.GetComponent<TextMesh>().text = "No tutorial text available.";
                    break;
            }
        }

        if (growing)
        {
            temp += new Vector3(0, Time.deltaTime / 2.0f, 0.1f);
            transform.localScale = temp;
        }

        if (shrinking && transform.localScale.y > 0.0f)
        {
            text.GetComponent<TextMesh>().text = " ";
            temp -= new Vector3(0, Time.deltaTime / 2.0f, 0.1f);
            transform.localScale = temp;
        }
        else if (shrinking)
        {
            shrinking = false;
            transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Guy").GetComponent<GuyMinigameScript>().done = true;
        }
    }
}
