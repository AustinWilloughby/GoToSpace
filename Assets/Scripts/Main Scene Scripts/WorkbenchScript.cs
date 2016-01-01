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
        //Very inefficient but working way to make the items fade
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CraftingMat"))
        {
            if (Vector2.Distance((Vector2)transform.position, (Vector2)obj.transform.position) < .2f)
            {
                if (StatusTracker.Instance != null)
                {
                    StatusTracker.Instance.CollectItem(obj.name);
                }
                obj.GetComponent<MaterialScript>().ObjectToWorkbench();
                obj.tag = "Untagged";
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    print(col.gameObject.tag);
    //    if (col.gameObject.tag == "CraftingMat")
    //    {
    //        if (StatusTracker.Instance != null)
    //        {
    //            StatusTracker.Instance.CollectItem(col.gameObject.name);
    //        }
    //        col.gameObject.GetComponent<MaterialScript>().ObjectToWorkbench();
    //        col.tag = "Untagged";
    //    }
    //}
}
