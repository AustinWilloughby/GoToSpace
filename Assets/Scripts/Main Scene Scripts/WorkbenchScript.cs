using UnityEngine;
using System.Collections;

public class WorkbenchScript : MonoBehaviour
{
    public int numberMaterials = 3;
    private int progress = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnColliderEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CraftingMat")
        {
            if(StatusTracker.Instance != null)
            {
                StatusTracker.Instance.CollectItem();
            }
            progress++;
            col.gameObject.GetComponent<MaterialScript>().ObjectToWorkbench(progress, numberMaterials);
            col.tag = "Untagged"; 
        }
    }
}
