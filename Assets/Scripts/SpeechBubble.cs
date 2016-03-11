using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour
{

    public GameObject text;
    Vector3 temp;
    public bool growing;
    public bool shrinking;
    public bool done;
    GameObject guy;
    public float waiting;
    private bool requiredSpeech;
    private bool talking;
    private string customSpeech;
    private bool shrinkNOW;
    private bool speechQueued;

    // Use this for initialization
    void Start()
    {
        growing = false;
        shrinking = false;
        text.GetComponent<TextMesh>().text = "";
        temp = new Vector3(.5f, 0, 0);
        transform.localScale = temp;
        done = false;
        waiting = 3.0f;
        shrinkNOW = false;
        requiredSpeech = false;
        guy = GameObject.Find("Guy");
    
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting > 0.0f && temp.y >= .6f)
        {
            waiting -= Time.deltaTime;
        }
        else if (waiting <= 0.0f)
        {
            growing = false;
            shrinkNOW = true;
            talking = false;
        }

        transform.position = guy.transform.position + new Vector3(1.9f, 1.9f, 0);

            // No Craftables on screen and there is still stuff left to craft
            if ((GameObject.FindGameObjectsWithTag("CraftingMat").GetLength(0) == 0 && GameObject.Find("StatusTracker").GetComponent<StatusTracker>().itemsNeeded.Count > 0) && !done)
            {
                growing = true;
            }

            // Special case for the first stage
            if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Platform && !done)
            {
                growing = true;
            }

            if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Skeleton && !done)
            {
                growing = true;
            }

            // Special case for the last stage
            if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.ToSpace && !done)
            {
                growing = true;
            }

            // Special case for stages with no minigame
            if (GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().shouldAdvanceStage && !done)
            {
                growing = true;
            }

        if (temp.y >= .6f)
        {
            growing = false;
            if (!talking)
            {
                requiredSpeech = true;
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Platform)
                    text.GetComponent<TextMesh>().text = "Let's Go To Space!\nGet the nails, wood, \nand hammer!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Skeleton)
                    text.GetComponent<TextMesh>().text = "We need to weld the \nmetal rods together!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Interior)
                    text.GetComponent<TextMesh>().text = "Let's go find a \nseat!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Interior && GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().shouldAdvanceStage)
                    text.GetComponent<TextMesh>().text = "Let's go find a \ndrill!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingFins)
                    text.GetComponent<TextMesh>().text = "Let's go find a \nsaw!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingWindow)
                    text.GetComponent<TextMesh>().text = "Let's go find a \nwindow!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingWindow && GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().shouldAdvanceStage)
                    text.GetComponent<TextMesh>().text = "Let's go find \npaint!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Painting && GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().shouldAdvanceStage)
                    text.GetComponent<TextMesh>().text = "Let's go find \nfuel!";
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.ToSpace)
                    text.GetComponent<TextMesh>().text = "Let's go to space!!";
            }

            if (!requiredSpeech)
            {
                text.GetComponent<TextMesh>().text = customSpeech;
            }
        }

        if (shrinkNOW)
        {
            growing = false;
            shrinking = true;
        }

        if (growing)
        {
            if(transform.localScale.y < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, 0, .1f);
                temp = transform.localScale;
            }
            temp += new Vector3(0, Time.deltaTime / 2.0f, .01f);
            transform.localScale = temp;
        }

        if (shrinking)
        {
            text.GetComponent<TextMesh>().text = " ";
            temp -= new Vector3(0, Time.deltaTime / 2.0f, .01f);
            transform.localScale = temp;
            if (transform.localScale.y < 0.0f)
            {
                shrinking = false;
                transform.localScale = new Vector3(transform.localScale.x, 0, .1f);
                requiredSpeech = false;
                done = false;
                shrinkNOW = false;
                if(speechQueued)
                {
                    talking = true;
                    waiting = 3.0f;
                    temp = transform.localScale;
                }
            }
        }

    }

    public void GuySays(string speech)
    {
        if (!talking)
        {
            if (!requiredSpeech && !growing && !shrinking && waiting <= 0.0f)
            {
                string formatted = "";
                string[] split = speech.Split(' ');
                int currentLength = 0;
                for (int i = 0; i < split.Length; i++)
                {
                    //Punctuation adds almost no length so we ignore it if we have to
                    if (!split[i].Contains("'") || !split[i].Contains(".") || !split[i].Contains(",") || !split[i].Contains("!")) 
                    {
                        if (currentLength + split[i].Length >= 18)
                        {
                            currentLength = split[i].Length + 1;
                            formatted += "\n" + split[i] + " ";
                        }
                        else
                        {
                            currentLength += split[i].Length + 1;
                            formatted += split[i] + " ";
                        }
                    }
                    else
                    {
                        if (currentLength + split[i].Length >= 19)
                        {
                            currentLength = split[i].Length;
                            formatted += "\n" + split[i] + " ";
                        }
                        else
                        {
                            currentLength += split[i].Length;
                            formatted += split[i] + " ";
                        }
                    }
                }

                text.GetComponent<TextMesh>().text = formatted;
                talking = true;
                customSpeech = formatted;
                shrinkNOW = false;
                waiting = 3.0f;
                temp = transform.localScale;
            }
            else
            {
                speechQueued = true;
            }
        }
    }

}
