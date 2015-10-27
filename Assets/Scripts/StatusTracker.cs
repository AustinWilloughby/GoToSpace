 using UnityEngine;
using System.Collections;

public class StatusTracker : MonoBehaviour
{
    #region Singleton Code
    //Fields
    private static StatusTracker instance = null;


    //Properties
    public static StatusTracker Instance
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

    public CurrentlyBuilding currentStage;

    //Sky Color Variables
    public Sprite[] skyColors;
    public int currentSkyColorIndex;
    private SpriteRenderer sky;

    // Use this for initialization
    void Start()
    {
        currentStage = CurrentlyBuilding.Platform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceStage()
    {
        currentStage++;
    }

    void OnLevelWasLoaded(int level) //Called whenever a new scene loads
    {
        TargetSkyBox();
    }

    private void TargetSkyBox() //Attempts to lock to scenes skybox
    {
        try //Try to find the skybox and set its color
        {
            sky = GameObject.Find("SkyColor").GetComponent<SpriteRenderer>();
            sky.sprite = skyColors[currentSkyColorIndex % skyColors.Length];
            
            //Possible final version, where each possible state has a sky color
            //sky.sprite = skyColors[(int)currentStage % skyColors.Length];
        }
        catch //If this fails, set the sky to null
        {
            sky = null;
        }
    }
}

public enum CurrentlyBuilding
{
    Platform,
    Skeleton,
    Interior,
    CrudeExterior,
    SandingExterior,
    AddingFins,
    AddingWindow,
    Painting,
    ToSpace
};
