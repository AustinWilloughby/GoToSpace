using UnityEngine;
using System.Collections;

public class FXHandler : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] soundFX;

    private MusicSingleton music;
    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        try
        {
            source.volume = MusicSingleton.Instance.volume;
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        CheckVolume();
    }

    public void PlayEffect(string sound)
    {
        int index = -1;
        for(int i = 0; i < soundFX.Length; i++)
        {
            if(soundFX[i].name == sound)
            {
                index = i;
            }
        }
        if(index > 0 && index < soundFX.Length)
        {
            source.clip = soundFX[index];
        }
        else
        {
            Debug.Log("Effect not found");
        }
    }

    private void CheckVolume()
    {
        try
        {
            if (source.volume != MusicSingleton.Instance.volume)
            {
                source.volume = MusicSingleton.Instance.volume;
            }
        }
        catch { }
    }
}
