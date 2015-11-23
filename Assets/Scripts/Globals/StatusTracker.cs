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
    public string[] miniGames;
    private SpriteRenderer sky;
    private int itemsLeft;

    public Sprite platform;
    public Sprite[] shipProgress;

    // Use this for initialization
    void Start()
    {
        currentStage = CurrentlyBuilding.Platform;
        itemsLeft = 3;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSky();
        UpdateShipSprite();
    }

    public void AdvanceStage()
    {
        currentStage = (CurrentlyBuilding)((int)currentStage + 1);
        currentSkyColorIndex++;
        itemsLeft = 3;
    }

    public void CollectItem()
    {
        itemsLeft--;
        if (itemsLeft <= 0)
        {
            try //Try to find the screenfader and load minigame with transition
            {
                GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[(int)currentStage]);
            }
            catch //If this fails, just load the minigame
            {
                Application.LoadLevel(miniGames[(int)currentStage]);
            }
        }
    }

    void OnLevelWasLoaded(int level) //Called whenever a new scene loads
    {
        TargetSkyBox();
        UpdateShipSprite();
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

    private void CheckSky()
    {
        if (sky != null)
        {
            if (sky.sprite != skyColors[currentSkyColorIndex % skyColors.Length])
            {
                sky.sprite = skyColors[currentSkyColorIndex % skyColors.Length];
            }
        }
        else
        {
            try
            {
                sky = GameObject.Find("SkyColor").GetComponent<SpriteRenderer>();
            }
            catch { }
        }
    }

    private void UpdateShipSprite()
    {
        if (GameObject.Find("ShipProgress") != null)
        {
            SpriteRenderer shipSprite = GameObject.Find("ShipProgress").GetComponent<SpriteRenderer>();
            SpriteRenderer platformSprite = GameObject.Find("ShipProgressPlatform").GetComponent<SpriteRenderer>();

            switch (currentStage)
            {
                case CurrentlyBuilding.Platform:
                    platformSprite.sprite = null;
                    shipSprite.sprite = null;
                    break;
                case CurrentlyBuilding.Skeleton:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = null;
                    break;
                case CurrentlyBuilding.Interior:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[0];
                    break;
                case CurrentlyBuilding.CrudeExterior:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[1];
                    break;
                case CurrentlyBuilding.SandingExterior:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[2];
                    break;
                case CurrentlyBuilding.AddingFins:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[3];

                    //----------REMOVE LATER, FOR TEST BUILD ----------
                    Application.LoadLevel("BlastOff");

                    break;
                case CurrentlyBuilding.AddingWindow:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[4];
                    break;
                case CurrentlyBuilding.ToSpace:
                    Application.LoadLevel("BlastOff");
                    break;
                default:
                    platformSprite = null;
                    shipSprite = null;
                    break;
            }
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
