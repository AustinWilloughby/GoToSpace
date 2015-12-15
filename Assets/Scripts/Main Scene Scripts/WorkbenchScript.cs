using UnityEngine;
using System.Collections;

public class WorkbenchScript : MonoBehaviour
{
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
            if(StatusTracker.Instance != null)
            {
                StatusTracker.Instance.CollectItem(col.gameObject.name);
            }
            col.gameObject.GetComponent<MaterialScript>().ObjectToWorkbench();
            col.tag = "Untagged"; 
        }
    }
}
