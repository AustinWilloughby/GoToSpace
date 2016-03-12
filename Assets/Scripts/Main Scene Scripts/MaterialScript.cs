using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    private bool fading;
    private bool detectingPickup;
    private bool held;
    private Vector4 color;
    private GameObject guy;
    public AudioClip pickupNoise;
    public string properName;
    public string dropOffText;

    //private int neededProg;
    //private int currentProg;
    // Use this for initialization
    void Start()
    {
        guy = GameObject.Find("Guy");
        fading = false;
        tag = "CraftingMat";

        try
        {
            if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().alreadyCollected.Contains(name))
            {
                Destroy(gameObject);
            }

            if (!GameObject.Find("StatusTracker").GetComponent<StatusTracker>().discoveredItems.Contains(name) && Application.loadedLevelName == "Workshop")
            {
                Destroy(gameObject);
            }
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            color.w -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = color;
            if (color.w <= 0)
            {
                GameObject.Find("StatusTracker").GetComponent<StatusTracker>().discoveredItems.Add(name);
                GameObject.Destroy(this);
            }
        }

        if (detectingPickup) { DetectPickup(); }
        if (held)
        {
            detectingPickup = false;
            transform.position = guy.transform.position;
            transform.position = transform.position + new Vector3(0, 0, -2.5f);

            if (Mathf.Abs(transform.position.x - GameObject.Find("Workbench").transform.position.x) < .01f)
            {
                if (dropOffText.Length > 0)
                {
                    GameObject.Find("SpeechBubble").GetComponent<SpeechBubble>().GuySays(dropOffText);
                }
                GameObject.Find("StatusTracker").GetComponent<StatusTracker>().alreadyCollected.Add(name);
                held = false;
                transform.position = GameObject.Find("Workbench").transform.position;
                transform.position = transform.position + new Vector3(0, 0, 0.5f);
            }
        }
    }

    public void ObjectToWorkbench()
    {
        if (!fading)
        {
            color = GetComponent<SpriteRenderer>().color;
            fading = true;
        }
    }

    void OnMouseOver()
    {
        try
        {
            if (Application.loadedLevelName == "Workshop")
            {
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().itemsNeeded.Contains(name))
                {
                    if (Input.GetMouseButtonDown(0) && !guy.GetComponent<GuyBehavior>().WalkingToPosition)
                    {
                        guy.GetComponent<GuyBehavior>().MakeWalkToPosition(transform.position);
                        detectingPickup = true;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (properName != null)
                        {
                            string text = "I don't need that " + properName.ToLower() + " right now!";
                            GameObject.Find("SpeechBubble").GetComponent<SpeechBubble>().GuySays(text);
                        }
                        else
                        {
                            GameObject.Find("SpeechBubble").GetComponent<SpeechBubble>().GuySays("I don't need that" + name.ToLower() + " right now!");
                        }
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().itemsNeeded.Contains(name))
                {
                    fading = true;
                    if (pickupNoise != null)
                    {
                        AudioSource source = gameObject.GetComponent<AudioSource>();
                        source.volume = GameObject.Find("MusicHandler").GetComponent<MusicSingleton>().volume;
                        source.clip = pickupNoise;
                        source.Play();
                    }
                }
                else
                {

                }
            }
        }
        catch { }
    }

    void DetectPickup()
    {
        if (!guy.GetComponent<GuyBehavior>().WalkingToPosition)
        {
            held = true;
            guy.GetComponent<GuyBehavior>().MakeWalkToPosition(GameObject.Find("Workbench").transform.position);
        }
    }
}
