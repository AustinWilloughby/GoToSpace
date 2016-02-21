using UnityEngine;
using System.Collections;

public class AmbienceSingleton : MonoBehaviour
{
    #region Singleton Code
    //Fields
    private static AmbienceSingleton instance = null;


    //Properties
    public static AmbienceSingleton Instance
    {
        get { return instance; }
    }


    //Methods
    void Awake() //Checks for other instance
    {
        lowPass = this.gameObject.GetComponent<AudioLowPassFilter>();
        source = this.gameObject.GetComponent<AudioSource>();
        source.Stop();
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

    public AudioClip[] ambience;
    private AudioLowPassFilter lowPass;
    private AudioSource source;
    private int currentIndex;

    // Use this for initialization
    void Start()
    {
        currentIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForUpdate();
    }

    void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
            case 3: //Street
            case 4: //Workshop
            case 5: //PaintShop
            case 6: //HardwareShop
            case 10: //Yard
                currentIndex = 0;
                CheckForUpdate();
                break;
                
            case 12: //InsideShip
                currentIndex = 1;
                CheckForUpdate();
                break;

            default:
                source.Stop();
                break;
        }

        switch(level)
        {
            case 4: //Workshop
            case 5: //PaintShop
            case 6: //HardwareShop
                lowPass.enabled = true;
                source.volume = .7f;
                break;

            case 12: //InsideShip
                lowPass.enabled = false;
                source.volume = .3f;
                break;

            default:
                lowPass.enabled = false;
                source.volume = .6f;
                break;
        }

    }

    private void CheckForUpdate()
    {
        if (currentIndex >= 0)
        {
            if (source.clip != ambience[currentIndex])
            {
                source.Stop();
                source.clip = ambience[currentIndex % ambience.Length];
                source.Play();
            }
        }
    }
}
