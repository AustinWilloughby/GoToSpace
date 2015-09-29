using UnityEngine;
using System.Collections;

public class WorkbenchScript : MonoBehaviour
{
    private int progress = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CraftingMat")
        {
            col.gameObject.GetComponent<MaterialScript>().ObjectToWorkbench();
            col.tag = "Untagged"; 
            progress++;
        }
    }
}
