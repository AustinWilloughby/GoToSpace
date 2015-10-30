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
    public float crossFadeTime = 1;

    public AudioSource audioPlayer;
    public AudioSource secondAudioPlayer;
    private int lastIndex;
    private bool fading = false;
    private int currentPlayer;
    private float crossFadeTimer;

    // Use this for initialization
    void Start()
    {
        crossFadeTimer = crossFadeTime;
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
        currentPlayer = 1;
        fading = false;
        audioPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentSongIndex != lastIndex)
        {
            ChangeCurrentMusic();
        }
        if (fading)
        {
            Crossfade();
        }
        else
        {
            CheckVolume();
        }
    }

    private void ChangeCurrentMusic()
    {
        crossFadeTimer = crossFadeTime;
        if(currentPlayer == 1)
        { 
            secondAudioPlayer.clip = music[currentSongIndex % music.Count];
            secondAudioPlayer.Play();
            currentPlayer = 2;
        }
        else
        {
            audioPlayer.clip = music[currentSongIndex % music.Count];
            currentPlayer = 1;
            audioPlayer.Play();
        }
        lastIndex = currentSongIndex;
        fading = true;
    }

    public void SetCurrentMusic(int index)
    {
        currentSongIndex = index;
    }
    
    public void SetCurrentMusic(string songName)
    {
        for(int i = 0; i < music.Count; i++)
        {
            if(music[i].name == songName)
            {
                currentSongIndex = i;
            }
        }
    }

    public bool IsCurrentSong(string songName)
    {
        if(music[currentSongIndex].name == songName)
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

    private void Crossfade()
    {
        crossFadeTimer -= Time.deltaTime;
        if(currentPlayer == 2)
        {
            audioPlayer.volume = Mathf.Lerp(0, volume, crossFadeTimer);
            secondAudioPlayer.volume = Mathf.Lerp(volume, 0, crossFadeTimer);
        }
        else
        {
            audioPlayer.volume = Mathf.Lerp(volume, 0, crossFadeTimer);
            secondAudioPlayer.volume = Mathf.Lerp(0, volume, crossFadeTimer);
        }
        if (crossFadeTimer <= 0)
        {
            fading = false;
            crossFadeTimer = crossFadeTime;
        }
    }

    private void CheckVolume()
    {
        if (currentPlayer == 1)
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
            if (secondAudioPlayer.volume != 0)
            {
                secondAudioPlayer.volume = 0;
                secondAudioPlayer.Stop();
            }
        }
        else
        {
            if (secondAudioPlayer.volume != volume)
            {
                if (volume > 1)
                {
                    volume = 1;
                }
                if (volume < 0)
                {
                    volume = 0;
                }
                secondAudioPlayer.volume = volume;
            }
            if(audioPlayer.volume != 0)
            {
                audioPlayer.volume = 0;
                audioPlayer.Stop();
            }
        }
    }
}
