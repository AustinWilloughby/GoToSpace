using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public float fadeRate = 1;
    public string newSong = null;

    private SpriteRenderer renderer;
    private Color tempColor;
    private bool fadeIn;
    private bool fadeOut;
    private string newSceneString;


    // Use this for initialization
    void Start()
    {
        FadeIn();
        if(newSong != null && MusicSingleton.Instance != null)
        {
            if (MusicSingleton.Instance.DoesSongExist(newSong) && !MusicSingleton.Instance.IsCurrentSong(newSong))
            {
                MusicSingleton.Instance.FadeOut(newSong);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(renderer == null)
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if(fadeIn)
        {
            renderer.enabled = true;
            tempColor = renderer.color;
            tempColor.a -= fadeRate * Time.deltaTime;
            renderer.color = tempColor;
            if(tempColor.a <= 0)
            {
                fadeIn = false;
            }
        }
        if(fadeOut)
        {
            tempColor = renderer.color;
            tempColor.a += fadeRate * Time.deltaTime;
            renderer.color = tempColor;
            if (tempColor.a >= 1)
            {
                fadeOut = false;
                if(newSceneString != null)
                {
                    Application.LoadLevel(newSceneString);
                }
            }
        }
    }

    public void FadeIn()
    {
        fadeOut = false;
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeIn = false;
        fadeOut = true;
    }

    public void FadeOut(string newScene)
    {
        FadeOut();
        newSceneString = newScene;
    }
}
