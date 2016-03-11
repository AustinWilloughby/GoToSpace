using UnityEngine;
using System.Collections;

public class WorldItemScript : MonoBehaviour 
{
    private bool fading;
    private bool detectingPickup;
    private bool held;
    private Vector4 color;
    private GameObject guy;
    public AudioClip pickupNoise;

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
            if (!GameObject.Find("StatusTracker").GetComponent<StatusTracker>().itemsNeeded.Contains(name))
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

            if (Application.loadedLevelName != "Yard")
            {
                if (Mathf.Abs(transform.position.x - GameObject.Find("register").transform.position.x) < .3f)
                {
                    held = false;
                    transform.position = GameObject.Find("register").transform.position;
                    transform.position = transform.position + new Vector3(0, 0, 2.5f);
                    fading = true;
                    if (pickupNoise != null)
                    {
                        AudioSource source = gameObject.GetComponent<AudioSource>();
                        source.volume = GameObject.Find("MusicHandler").GetComponent<MusicSingleton>().volume;
                        source.clip = pickupNoise;
                        source.Play();
                    }
                }
            }
            else
            {
                if (Mathf.Abs(transform.position.x - GameObject.Find("House").transform.position.x) < .03f)
                {
                    GameObject.Find("StatusTracker").GetComponent<StatusTracker>().discoveredItems.Add(name);;
                    GameObject.Find("Guy").GetComponent<GuyBehavior>().StandStill(10.0f);
                    GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut("Workshop");
                }
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
            if (Input.GetMouseButtonDown(0) && !guy.GetComponent<GuyBehavior>().WalkingToPosition)
            {
                guy.GetComponent<GuyBehavior>().MakeWalkToPosition(transform.position);
                detectingPickup = true;
            }

        }
        catch { }
    }

    void DetectPickup()
    {
        if (!guy.GetComponent<GuyBehavior>().WalkingToPosition)
        {
            held = true;
            if (Application.loadedLevelName != "Yard")
            {
                guy.GetComponent<GuyBehavior>().MakeWalkToPosition(GameObject.Find("register").transform.position);
            }
            else
            {
                guy.GetComponent<GuyBehavior>().MakeWalkToPosition(GameObject.Find("House").transform.position);
            }
        }
    }
}
