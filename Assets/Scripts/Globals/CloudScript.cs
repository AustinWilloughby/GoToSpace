﻿using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour
{
    public float speed;
    public float scale;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x += speed * Time.deltaTime;
        transform.position = newPos;

        if (newPos.x > 25.0f * scale)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
