using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectBounding : MonoBehaviour
{
    public List<GameObject> boundedObjects;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //This needs to be improved. Only triggers once the object completely leaves the bounding box. Should
        //trigger as soon as any part of it leaves the bounding box. Then it also snaps it all the way back into
        //the bounding box and looks jarring and snappy.
        if (boundedObjects.Contains(col.gameObject))
        {
            Vector2 fromObjToThis = (Vector2)transform.position - (Vector2)col.gameObject.transform.position;
            fromObjToThis = fromObjToThis.normalized * col.bounds.size.magnitude;
            col.gameObject.transform.position += (Vector3)fromObjToThis;
        }
    }

}
