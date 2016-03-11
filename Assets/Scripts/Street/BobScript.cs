using UnityEngine;
using System.Collections;

public class BobScript : MonoBehaviour
{
    private bool facingRight;
    private float timer;
    private GameObject guy;
    private bool sayHello;

    // Use this for initialization
    void Start()
    {
        guy = GameObject.Find("Guy");
    }

    // Update is called once per frame
    void Update()
    {
        FaceGuy();
        //RandomFlip();
        HandleFlipping();
        if(!sayHello)
        {
            sayHello = true;
            GameObject.Find("SpeechBubble").GetComponent<SpeechBubble>().GuySays("Welcome to my happy little shop!");
        }
    }

    private void HandleFlipping()
    {
        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void RandomFlip()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(1.0f, 2.0f);
            if (Random.Range(0, 3) == 0)
            {
                facingRight = !facingRight;
            }
        }
    }

    private void FaceGuy()
    {
        if (guy.transform.position.x >= transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }
}
