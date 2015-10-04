using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicSingleton : MonoBehaviour
{
    #region Singleton Code
    //Fields
    private static MusicSingleton instance = null;


    //Properties
    public static MusicSingleton Instance
    {
        get { return instance; }
    }


    //Methods
    void Awake() //Checks for other instance
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    //Fields
    public float volume;
    public int currentSongIndex;
    public List<AudioClip> music;

    private AudioSource audioPlayer;
    private int lastIndex;

    // Use this for initialization
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        if (audioPlayer.volume != volume)
        {
            if (volume > 1)
            {
                volume = 1;
            }
            if (volume < 0)
            {
                volume = 0;
            }
        }
        audioPlayer.volume = volume;
        audioPlayer.clip = music[currentSongIndex % music.Count];
        lastIndex = currentSongIndex;
        audioPlayer.loop = true;
        audioPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentSongIndex != lastIndex)
        {
            ChangeCurrentMusic();
        }
        CheckVolume();
    }

    private void ChangeCurrentMusic()
    {
        audioPlayer.Stop();
        audioPlayer.clip = music[currentSongIndex % music.Count];
        audioPlayer.Play();
        lastIndex = currentSongIndex;
    }

    private void CheckVolume()
    {
        if (audioPlayer.volume != volume)
        {
            if (volume > 1)
            {
                volume = 1;
            }
            if (volume < 0)
            {
                volume = 0;
            }
            audioPlayer.Pause();
            audioPlayer.volume = volume;
            audioPlayer.UnPause();
        }
    }
}
