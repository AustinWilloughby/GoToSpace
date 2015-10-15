using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    private bool fading;
    private bool detectingPickup;
    private bool held;
    private Vector4 color;
    private GameObject guy;

    private int neededProg;
    private int currentProg;
    // Use this for initialization
    void Start()
    {
        guy = GameObject.Find("Guy");
        fading = false;
        tag = "CraftingMat";
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
                if (currentProg == neededProg)
                {
                    Application.LoadLevel("Minigame1");
                }
                else
                {
                    GameObject.Destroy(this);
                }
            }
        }

        if (detectingPickup) { DetectPickup(); }
        if (held)
        {
            detectingPickup = false;
            transform.position = guy.transform.position;
            transform.position = transform.position + new Vector3(0, 0, -2.5f);

            if (Mathf.Abs(transform.position.x - GameObject.Find("Workbench").transform.position.x) < .2f)
            {
                transform.position = GameObject.Find("Workbench").transform.position;
                transform.position = transform.position + new Vector3(0, 0, -2.5f);
            }
        }
    }

    public void ObjectToWorkbench(int currentProgress, int neededProgress)
    {
        if (!fading)
        {
            neededProg = neededProgress;
            currentProg = currentProgress;
            color = GetComponent<SpriteRenderer>().color;
            fading = true;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !guy.GetComponent<GuyBehavior>().WalkingToPosition)
        {
            guy.GetComponent<GuyBehavior>().MakeWalkToPosition(transform.position);
            detectingPickup = true;
        }
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
