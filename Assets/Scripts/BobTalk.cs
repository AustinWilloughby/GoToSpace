using UnityEngine;
using System.Collections;

public class BobTalk : MonoBehaviour
{
    private float timer;
    private int stage;
    private Vector3 temp;
    public MeshRenderer text;

    // Use this for initialization
    void Start()
    {
        timer = 1.0f;
        transform.localScale = new Vector3(transform.localScale.x, 0, .1f);
        temp = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        switch (stage)
        {
            case 0:
                if(timer <= 0)
                {
                    stage++;
                }
                break;
            case 1:

                temp += new Vector3(0, Time.deltaTime / 2.0f, .01f);
                transform.localScale = temp;

                if(transform.localScale.y > .7)
                {
                    stage++;
                    timer = 3.0f;
                    text.enabled = true;
                }
                break;
            case 2:
                if(timer <= 0)
                {
                    stage++;
                    text.enabled = false;
                }

                break;
            default:
                temp -= new Vector3(0, Time.deltaTime / 2.0f, .01f);
                transform.localScale = temp;

                if(transform.localScale.y < 0.0f)
                {
                    print("dank");
                    GameObject.Destroy(gameObject);
                }
                break;
        }
    }
}
