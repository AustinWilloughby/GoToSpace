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
    public float fadeTime = .5f;
    public AudioSource audioPlayer;

    private int lastIndex;
    private float fadeTimer = 1;
    private bool fadingOut = false;
    private bool fadingIn = false;
    private string upcomingSong;

    // Use this for initialization
    void Start()
    {
        fadeTimer = fadeTime;
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
        if (fadingOut)
        {
            fadeTimer -= Time.deltaTime;
            audioPlayer.volume = Mathf.Lerp(0, volume, fadeTimer);
            if(fadeTimer <= 0)
            {
                fadingOut = false;
                if(upcomingSong != null)
                {
                    SetCurrentMusic(upcomingSong);
                    upcomingSong = null;
                }
                FadeIn();
                
            }
        }
        else if (fadingIn)
        {
            fadeTimer -= Time.deltaTime;
            audioPlayer.volume = Mathf.Lerp(volume, 0, fadeTimer);
            if (fadeTimer <= 0)
            {
                fadingIn = false;
            }
        }
        else
        {
            CheckVolume();
        }

        if (currentSongIndex != lastIndex)
        {
            ChangeCurrentMusic();
        }
    }

    private void ChangeCurrentMusic()
    {
        audioPlayer.Stop();
        audioPlayer.clip = music[currentSongIndex % music.Count];
        audioPlayer.Play();
        lastIndex = currentSongIndex;
    }

    public void SetCurrentMusic(int index)
    {
        currentSongIndex = index;
    }

    public void SetCurrentMusic(string songName)
    {
        for (int i = 0; i < music.Count; i++)
        {
            if (music[i].name == songName)
            {
                currentSongIndex = i;
            }
        }
    }

    public bool IsCurrentSong(string songName)
    {
        if (music[currentSongIndex].name == songName)
        {
            return true;
        }
        return false;
    }

    public bool DoesSongExist(string songName)
    {
        for (int i = 0; i < music.Count; i++)
        {
            if (music[i].name == songName)
            {
                return true;
            }
        }
        return false;
    }

    public void FadeOut()
    {
        fadingIn = false;
        fadingOut = true;
        fadeTimer = fadeTime;
    }

    public void FadeOut(string newSong)
    {
        FadeOut();
        upcomingSong = newSong;
    }

    private void FadeIn()
    {
        fadingOut = false;
        fadingIn = true;
        fadeTimer = fadeTime;
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
            audioPlayer.volume = volume;
        }
    }

}

