using UnityEngine;
using System.Collections;

public class NailGameController : MonoBehaviour {

    public GameObject nail1;
    public GameObject nail2;
    public GameObject nail3;
    public GameObject nail4;

    private TextMesh textMesh;
    private bool didAdvanceStage;
    private bool waiting;
    private float waitTime;
    private string grade;
    private int hits;
    private int misses;

	// Use this for initialization
	void Start () {
        textMesh = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        textMesh.text = "";
        didAdvanceStage = false;
        waiting = true;
        waitTime = 2.0f;
        grade = "F";

        hits = 0;
        misses = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!waiting)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && Input.GetMouseButton(0))
        {
            Advance();
        }

        CheckStatus();
	}

    private void CheckStatus()
    {
		if ((nail1.GetComponent<NailScript>().hits > 3 || nail1.GetComponent<NailScript>().misses > 2) &&
		    (nail2.GetComponent<NailScript>().hits > 3 || nail2.GetComponent<NailScript>().misses > 2) &&
			(nail3.GetComponent<NailScript>().hits > 3 || nail3.GetComponent<NailScript>().misses > 2) &&
			(nail4.GetComponent<NailScript>().hits > 3 || nail4.GetComponent<NailScript>().misses > 2))
        {
            AssessGrade();
            DisplayScoreText();
            waiting = false;
        }
    }

    private void AssessGrade()
    {
        hits = nail1.GetComponent<NailScript>().hits +
               nail2.GetComponent<NailScript>().hits +
               nail3.GetComponent<NailScript>().hits +
               nail4.GetComponent<NailScript>().hits;

        misses = nail1.GetComponent<NailScript>().misses +
                 nail2.GetComponent<NailScript>().misses +
                 nail3.GetComponent<NailScript>().misses +
                 nail4.GetComponent<NailScript>().misses;

        switch (misses)
        {
            case 0:
                grade = "S";
                break;
            case 1:
                grade = "A";;
                break;
            case 2:
            case 3:
                grade = "B";;
                break;
            case 4:
            case 5:
                grade = "C";
                break;
            case 6:
            case 7:
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
        textMesh.text = "End Stage!\n\nHits: " + hits + "\nMisses: " + misses + "\n\nGrade: " + grade;
        GameObject.Find("ScoreAudio").GetComponent<ScoreAudio>().PlayTune(grade);
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
