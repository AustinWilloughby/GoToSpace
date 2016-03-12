using UnityEngine;
using System.Collections;

public class SawPoint : MonoBehaviour {

    bool outOfBounds;
    bool onScratch;
	bool goingToPoint2;
	public int progress;
    public GameObject scratch;
    public float outOfBoundsTime;

    private TextMesh textMesh;
    private bool didAdvanceStage;
    private bool waiting;
    private float waitTime;
    private string grade;

	// Use this for initialization
	void Start () {
        onScratch = false;
        outOfBounds = false;
		goingToPoint2 = false;
		progress = 0;
        outOfBoundsTime = 0;

        textMesh = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        textMesh.text = "";
        didAdvanceStage = false;
        waiting = true;
        waitTime = 2.0f;
        grade = "F";
	}
	
	// Update is called once per frame
	void Update () {
        if (outOfBounds && waiting)
        {
            outOfBoundsTime += Time.deltaTime;
        }
        if (!waiting)
        {
            waitTime -= Time.deltaTime;
        }
        if (progress > 24)
        {
            AssessGrade();
            DisplayScoreText();
            waiting = false;
        }
        if (waitTime <= 0 && Input.GetMouseButton(0))
        {
            Advance();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            outOfBounds = true;
        }

		if (other.tag == "Point1" && !goingToPoint2)
		{
			progress += 1;
			goingToPoint2 = true;
		}

		if (other.tag == "Point2" && goingToPoint2)
		{
			progress += 1;
			goingToPoint2 = false;
		}

        if (other.tag == "Scratch")
        {
            onScratch = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            if (!onScratch)
            {
                Vector3 temp = transform.position;
                temp += new Vector3(0, 0, 1.0f);
                Instantiate(scratch, temp, Quaternion.identity);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            outOfBounds = false;
        }

        if (other.tag == "Scratch")
        {
            onScratch = false;
        }

		if (other.tag == "Bound")
		{
			progress = 0;
		}
    }
    private void AssessGrade()
    {
        if (outOfBoundsTime <= 0.1f)
        {
            grade = "S";
        }
        else if (outOfBoundsTime <= 0.5f)
        {
            grade = "A";
        }
        else if (outOfBoundsTime <= 1.0f)
        {
            grade = "B";
        }
        else if (outOfBoundsTime <= 2.0f)
        {
            grade = "C";
        }
        else if (outOfBoundsTime <= 3.0f)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }
    }

    private void DisplayScoreText()
    {
        GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().fadeOut = true;
        textMesh.text = "End Stage!\n\nTime Out of Bounds: " + Mathf.Round(outOfBoundsTime * 100f) / 100f + "\n\nGrade: " + grade;
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
