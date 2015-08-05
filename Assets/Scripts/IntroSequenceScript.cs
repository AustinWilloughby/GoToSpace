using UnityEngine;
using System.Collections;

public class IntroSequenceScript : MonoBehaviour
{
    //Fields
    //Public
    public float transitionTime;
    public float logoTimer;

    public GameObject logo;
    public GameObject titleScreen;
    public GameObject cameraDestination;

    //Private
    private int introStep = 0;
    private float currentTime;

    // Use this for initialization
    void Start()
    {
        currentTime = transitionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < 0)
        {
            introStep++;
        }

        if (introStep < 5)
        {
            switch (introStep)
            {
            }
        }
    }
}
