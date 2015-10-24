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
