using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    public float waitTimer = 2.0f;
    float fadeTimer1 = 1.0f;
    float fadeTimer2 = 1.0f;
    float fadeTimer3 = 1.0f;
    float tipTimer = 0.1f;
    float fadeTimer4 = 1.0f;
    float doubleTapTime = .5f;
    private float doubleTapTimer;
    private bool firstTap = false;

    ArrayList starArray = new ArrayList();
    public GameObject stars1 = null;
    public GameObject stars2 = null;
    public GameObject stars3 = null;
    public GameObject stars4 = null;
    public GameObject stars5 = null;

    public GameObject fedora = null;
    public GameObject mainStars = null;
    public GameObject logo = null;

	// Use this for initialization
	void Start () {
        starArray.Add(stars1);
        starArray.Add(stars2);
        starArray.Add(stars3);
        starArray.Add(stars4);
        starArray.Add(stars5);

        doubleTapTimer = doubleTapTime;

        fedora.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        logo.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Double tap to skip splash sequence.
        if (firstTap)
        {
            doubleTapTimer -= Time.deltaTime;
            if (doubleTapTimer <= 0)
            {
                firstTap = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("Title");
            }
        }

        if (Input.GetMouseButtonDown(0) && firstTap == false)
        {
            firstTap = true;
            doubleTapTimer = doubleTapTime;
        }

        if (waitTimer >= 0.0f)
        {
            waitTimer -= Time.deltaTime;
        }
        else if (fadeTimer1 >= 0.0f)
        {
            fadeTimer1 -= Time.deltaTime * .5f;

            foreach (GameObject star in starArray)
            {
                star.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer1);
            }
        }
        else if (fadeTimer2 >= 0.0f)
        {
            fadeTimer2 -= Time.deltaTime * .5f;

            mainStars.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer2);
            fedora.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f - fadeTimer2);
        }
        else if (fadeTimer3 >= 0.0f)
        {
            fadeTimer3 -= Time.deltaTime * .5f;
            logo.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f - fadeTimer3);
        }
        else if (tipTimer >= 0.0f)
        {
            tipTimer -= Time.deltaTime * .5f;
            if (!GameObject.Find("Fedora").GetComponent<Tip>().doTip) 
            {
                GameObject.Find("Fedora").GetComponent<Tip>().doTip = true;
            }
        }
        else if (fadeTimer4 >= 0.0f)
        {
            fadeTimer4 -= Time.deltaTime * .5f;
            fedora.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer4);
            logo.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeTimer4);
        }
        else
        {
            Application.LoadLevel("Title");
        }
	}
}
