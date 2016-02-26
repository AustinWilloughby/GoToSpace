using UnityEngine;
using System.Collections;

public class IntroTransition : MonoBehaviour
{
    private SpriteRenderer screenBlack;
    public float timeScale = 1;
    public bool madeIt = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (screenBlack == null)
        {
            screenBlack = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        if (screenBlack.color.a > 0)
        {
            Color newColor = screenBlack.color;
            newColor.a -= 1f * Time.deltaTime;
            screenBlack.color = newColor;
        }
        Vector3 temp = transform.position;
        if (transform.position.y > 4.6f && madeIt == false)
        {
            temp.y -= .04f * timeScale;
            transform.position = temp;
            if (transform.position.y < 4.6f)
            {
                transform.position = new Vector3(transform.position.x, 4.6f, transform.position.z);
            }
        }
        else
        {
            madeIt = true;
        }
    }
}
