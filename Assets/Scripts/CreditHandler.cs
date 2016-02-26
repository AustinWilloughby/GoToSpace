using UnityEngine;
using System.Collections;

public class CreditHandler : MonoBehaviour
{
    public float pauseTimer;
    public float distance;
    public float slideTimer;
    public CloudGenerator[] clouds;
    public GameObject credits;

    private bool runSequence;
    private int sequenceSection = 0;
    private Camera cam;
    private float doubleTapTime = .5f;
    private float doubleTapTimer;
    private bool firstTap = false;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        runSequence = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (runSequence)
        {
            Vector3 camPos;
            Vector2 credPos;
            switch (sequenceSection)
            {
                case 0:
                    for (int i = 0; i < clouds.Length; i++)
                    {
                        //clouds[i].enabled = false;
                    }
                    sequenceSection++;
                    break;

                case 1:
                    camPos = cam.gameObject.transform.position;
                    camPos.y += Time.deltaTime * 5.0f;
                    cam.gameObject.transform.position = camPos;
                    if (camPos.y > 18.2)
                    {
                        sequenceSection++;
                    }
                    break;

                case 2:
                    credPos = credits.transform.position;
                    credPos.x += Time.deltaTime * 3;
                    credits.transform.position = credPos;
                    if(credPos.x > 150.0f)
                    {
                        sequenceSection++;
                    }
                    break;

                case 3:
                    camPos  = cam.gameObject.transform.position;
                    camPos.y -= Time.deltaTime * 3.5f;
                    cam.gameObject.transform.position = camPos ;
                                        
                    credPos = credits.transform.position;
                    credPos.x += Time.deltaTime * 3;
                    credits.transform.position = credPos;

                    if (camPos.y < 4.62)
                    {
                        sequenceSection++;
                    }
                    break;

                default:
                    runSequence = false;
                    sequenceSection = 0;

                    credPos = credits.transform.position;
                    credPos.x = -26.6f;
                    credits.transform.position = credPos;
                    break;
            }

            if (firstTap)
            {
                doubleTapTimer -= Time.deltaTime;
                if (doubleTapTimer <= 0)
                {
                    firstTap = false;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    sequenceSection = 3;
                }
            }

            if (Input.GetMouseButtonDown(0) && firstTap == false)
            {
                firstTap = true;
                doubleTapTimer = doubleTapTime;
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(runSequence == true)
            {
                sequenceSection = 3;
            }
            runSequence = true;
        }
    }
}
