using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour
{
    public float highlightDelay;
    public float flashInterval;

    private float flashTimer;
    private bool fadingIn;
    private SpriteRenderer renderer;
    private bool active;

    // Use this for initialization
    void Start()
    {
        flashTimer = flashInterval;
        fadingIn = true;
        renderer = GetComponent<SpriteRenderer>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (highlightDelay > 0)
            {
                highlightDelay -= Time.deltaTime;
            }
            else
            {
                flashTimer -= Time.deltaTime;
                if (flashTimer <= 0)
                {
                    flashTimer = flashInterval;
                    fadingIn = !fadingIn;
                }

                Color spriteAlpha = renderer.color;
                if (fadingIn)
                {
                    spriteAlpha.a = Mathf.Lerp(1.3f, -1, flashTimer / flashInterval);
                }
                else
                {
                    spriteAlpha.a = Mathf.Lerp(-1, 1.3f, flashTimer / flashInterval);
                }
                renderer.color = spriteAlpha;
            }
        }
        else
        {
            if (renderer.color.a > 0)
            {
                Color spriteAlpha = renderer.color;
                spriteAlpha.a -= Time.deltaTime;
                renderer.color = spriteAlpha;
            }
        }
    }

    public void StopFlash()
    {
        active = false;

    }
}
