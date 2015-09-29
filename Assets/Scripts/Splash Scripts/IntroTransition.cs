using UnityEngine;
using System.Collections;

public class IntroTransition : MonoBehaviour
{

    public float timeScale = 1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        if (transform.position.y > -.39f)
        {
            temp.y -= .04f * timeScale;
            transform.position = temp;
            if (transform.position.y < -.39f)
            {
                transform.position = new Vector3(transform.position.x, -.39f, transform.position.z);
            }
        }
    }
}
