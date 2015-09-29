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

    void OnColliderExit2D(Collider2D col)
    {
        if (boundedObjects.Contains(col.gameObject))
        {
            col.gameObject.transform.position = transform.position;
        }
    }
}
