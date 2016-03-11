using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public List<string> itemsNeeded;
	public List<string> discoveredItems;

    public Sprite platform;
    public Sprite[] shipProgress;
    private int lastLevelInt;
    private int thisLevel;

    private AudioSource source;
    public AudioClip[] construction;

    public int LastLevelInt
    {
        get { return lastLevelInt; }
    }
    public int ThisLevel
    {
        get { return thisLevel; }
    }

    // Use this for initialization
    void Start()
    {
        currentStage = CurrentlyBuilding.Platform;

		discoveredItems = new List<string> ();
		// Platform
		// All items for first stage discovered by default
		discoveredItems.Add ("Nails");
		discoveredItems.Add ("Wood");
		discoveredItems.Add ("Hammer");

		// Skeleton
		// Torch needs to be found in hardware store
		//discoveredItems.Add("Torch");
		discoveredItems.Add("MetalRod");

		// Interior
		// Car Seat needs to be found from the car
		discoveredItems.Add("SocketWrench");
		discoveredItems.Add("Bolts");
		//discoveredItems.Add("CarSeat");

		// CrudeExterior
		// Drill needs to be found in hardware store
		discoveredItems.Add("SheetMetal");
		//discoveredItems.Add("Drill");
		discoveredItems.Add("Screws");

		// AddingFins
		// Saw needs to be found in hardware store
		//discoveredItems.Add("Saw");

		// AddingWindow
		// Window needs to be found from the car
		//discoveredItems.Add("Window");

		// Painting
		// Paint needs to be found from paint store
		//discoveredItems.Add("Paint");
		discoveredItems.Add("PaintRoller");

		// Fueling
		// Fuel needs to be found in paint store
		//discoveredItems.Add("Fuel");

		itemsNeeded = new List<string> ();
		//Nail Game items to start off with
		itemsNeeded.Add ("Nails");
		itemsNeeded.Add ("Wood");
		itemsNeeded.Add ("Hammer");

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
        CheckSky();
        UpdateShipSprite();
    }

    public void AdvanceStage()
    {
        currentStage = (CurrentlyBuilding)((int)currentStage + 1);
        source.clip = construction[Random.Range(0, construction.Length - 1)];
        source.Play();
        currentSkyColorIndex++;

		itemsNeeded.Clear ();

		switch (currentStage) {
			//Welding
		case CurrentlyBuilding.Skeleton:
			itemsNeeded.Add("Torch");
			itemsNeeded.Add("MetalRod");
			break;
			//No Game
		case CurrentlyBuilding.Interior:
			itemsNeeded.Add("SocketWrench");
			itemsNeeded.Add("Bolts");
			itemsNeeded.Add("CarSeat");
			break;
			//Drilling
		case CurrentlyBuilding.CrudeExterior:
			itemsNeeded.Add("SheetMetal");
			itemsNeeded.Add("Drill");
			itemsNeeded.Add("Screws");
			break;
			//Sawing
		case CurrentlyBuilding.AddingFins:
			itemsNeeded.Add("Saw");
			break;
			//No Game
		case CurrentlyBuilding.AddingWindow:
			itemsNeeded.Add("Window");
			break;
			//No Game
		case CurrentlyBuilding.Painting:
			itemsNeeded.Add("Paint");
			itemsNeeded.Add("PaintRoller");
			break;
			//Fueling
		case CurrentlyBuilding.Fueling:
			itemsNeeded.Add("Fuel");
			break;
		case CurrentlyBuilding.ToSpace:
			break;
		default:
			break;
		}
    }

    public void CollectItem(string itemName)
    {
		Debug.Log ("Removed " + itemName);
		itemsNeeded.Remove (itemName);
        if (itemsNeeded.Count <= 0)
        {
			Debug.Log ("No more items needed");

			if (currentStage == CurrentlyBuilding.Platform){
           		GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[0]);
			}
			else if (currentStage == CurrentlyBuilding.Skeleton){
				GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[1]);
			} 
			else if (currentStage == CurrentlyBuilding.CrudeExterior){
				GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[2]);
			}
			else if (currentStage == CurrentlyBuilding.AddingFins){
				GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[3]);
			} 
			else if (currentStage == CurrentlyBuilding.Fueling){
				GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut(miniGames[4]);
			}
			else {
				GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().shouldAdvanceStage = true;
				Debug.Log ("No minigame, will advance stage on exit");
			}
        }
    }

    void OnLevelWasLoaded(int level) //Called whenever a new scene loads
    {
        lastLevelInt = thisLevel;
        thisLevel = level;
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
                case CurrentlyBuilding.AddingFins:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[2];

                    //----------REMOVE LATER, FOR TEST BUILD ----------
                    //Application.LoadLevel("BlastOff");

                    break;
                case CurrentlyBuilding.AddingWindow:
                    platformSprite.sprite = platform;
                    shipSprite.sprite = shipProgress[3];
                    break;
				case CurrentlyBuilding.Painting:
					platformSprite.sprite = platform;
					shipSprite.sprite = shipProgress[4];
				break;
				case CurrentlyBuilding.Fueling:
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
    AddingFins,
    AddingWindow,
    Painting,
	Fueling,
    ToSpace
};
