using UnityEngine;
using System.Collections;

public class WeldProgress : MonoBehaviour {

	GameObject[] weldTargets;
	public int completed;
	public int failed;
    public int incomplete;

    private TextMesh textMesh;
    private bool didAdvanceStage;
    private bool waiting;
    private float waitTime;
    private string grade;

	// Use this for initialization
	void Start () {
		weldTargets = GameObject.FindGameObjectsWithTag ("WeldTarget");
		completed = 0;
		failed = 0;
        incomplete = 0;

        textMesh = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        textMesh.text = "";
        didAdvanceStage = false;
        waiting = true;
        waitTime = 2.0f;
        grade = "F";
	}
	
	// Update is called once per frame
	void Update () {
        if (!waiting)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && Input.GetMouseButton(0))
        {
            Advance();
        }

        checkProgress();
	}

	void checkProgress()
	{
        completed = 0;
        failed = 0;
        incomplete = 0;
        for (int i = 0; i < weldTargets.Length; i++)
        {
            if (weldTargets[i].GetComponent<WeldTarget>().completed)
            {
                completed++;
            }
        
            else if (weldTargets[i].GetComponent<WeldTarget>().failed)
            {
                failed++;
            }
        
            else
            {
                incomplete++;
            }
        }
        if (incomplete <= 0)
        {
            AssessGrade();
            DisplayScoreText();
            waiting = false;
        }
	}

    private void AssessGrade()
    {
        switch (failed)
        {
            case 0:
                grade = "S";
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                grade = "A";
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
                grade = "B";
                break;
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
                grade = "C";
                break;
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
            case 23:
                grade = "D";
                break;
            default:
                grade = "F";
                break;
        }
    }

    private void DisplayScoreText()
    {
        GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().fadeOut = true;
        textMesh.text = "End Stage!\n\nProper Welds: " + completed + "\nImproper Welds: " + failed + "\n\nGrade: " + grade;
    }

    private void Advance()
    {
        if (StatusTracker.Instance != null && !didAdvanceStage)
        {
            StatusTracker.Instance.AdvanceStage();
            didAdvanceStage = true;
        }
        GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().FadeOut("Workshop");
    }
}
