using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    private bool fading;
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
            Debug.Log("Walk to Item");
        }
    }
}
