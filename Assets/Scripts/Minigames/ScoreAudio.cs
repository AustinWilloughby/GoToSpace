using UnityEngine;
using System.Collections;

public class ScoreAudio : MonoBehaviour
{
    public AudioClip[] scores;
    private AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        try
        {
            source.volume = GameObject.Find("AmbientSounds").GetComponent<AmbienceSingleton>().volume;
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTune(string grade)
    {
        source.Stop();
        switch (grade)
        {
            case "S":
                source.clip = scores[0];
                break;
            case "A":
            case "B":
            case "C":
            case "D":
                source.clip = scores[1];
                break;
            default:
                source.clip = scores[2];
                break;
        }
        source.Play();
    }
}
