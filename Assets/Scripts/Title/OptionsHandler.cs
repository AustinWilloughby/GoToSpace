using UnityEngine;
using System.Collections;

public class OptionsHandler : MonoBehaviour
{
    private bool runSequence;
    private int sequenceSection = 0;
    private Camera cam;
    public GameObject[] options;
    public GameObject[] menu;

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
            switch (sequenceSection)
            {
                case 0:
                    for(int i = 0; i < options.Length; i++)
                    {
                        options[i].GetComponent<SpriteRenderer>().enabled = true;
                    }
                    sequenceSection++;
                    break;

                case 1:
                    camPos = cam.gameObject.transform.position;
                    camPos.y += Time.deltaTime * 5.0f;
                    cam.gameObject.transform.position = camPos;
                    options[2].GetComponent<BoxCollider2D>().enabled = true;
                    options[3].GetComponent<BoxCollider2D>().enabled = true;
                    if (camPos.y > 18.2)
                    {
                        sequenceSection++;
                        Color alphaStart = menu[0].GetComponent<SpriteRenderer>().color;
                        alphaStart.a = 0;
                        for(int i = 0; i < menu.Length; i++)
                        {
                            menu[i].GetComponent<SpriteRenderer>().color = alphaStart;
                            menu[i].GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                    break;

                case 2:
                    Color alpha = menu[0].GetComponent<SpriteRenderer>().color;
                    alpha.a += Time.deltaTime;
                    for (int i = 0; i < menu.Length; i++)
                    {
                        menu[i].GetComponent<SpriteRenderer>().color = alpha;
                    }
                    if(alpha.a >= 1)
                    {
                        menu[0].GetComponent<BoxCollider2D>().enabled = true;
                        sequenceSection++;
                    }
                    break;

                case 3:

                    break;

                case 4:
                    menu[0].GetComponent<BoxCollider2D>().enabled = false;
                    options[2].GetComponent<BoxCollider2D>().enabled = false;
                    options[3].GetComponent<BoxCollider2D>().enabled = false;
                    camPos = cam.gameObject.transform.position;
                    camPos.y -= Time.deltaTime * 3.5f;
                    cam.gameObject.transform.position = camPos;

                    Color alph = menu[0].GetComponent<SpriteRenderer>().color;
                    alph.a -= Time.deltaTime;

                    for (int i = 0; i < menu.Length; i++)
                    {
                        menu[i].GetComponent<SpriteRenderer>().color = alph;
                    }


                    if (camPos.y < 4.62)
                    {
                        sequenceSection++;
                        for (int i = 0; i < options.Length; i++)
                        {
                            options[i].GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                    break;

                default:
                    runSequence = false;
                    sequenceSection = 0;
                    break;
            }
        }
    }

    public void DoMenuButton()
    {
        if(sequenceSection == 3)
        {
            sequenceSection++;
        }
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (runSequence == true)
            {
                sequenceSection = 3;
            }
            runSequence = true;
        }
    }
}
