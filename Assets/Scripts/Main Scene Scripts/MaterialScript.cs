using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    private bool fading;
    private Vector4 color;

    // Use this for initialization
    void Start()
    {
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
                GameObject.Destroy(this);
            }
        }
    }

    public void ObjectToWorkbench()
    {
        color = GetComponent<SpriteRenderer>().color;
        fading = true;
    }
}
