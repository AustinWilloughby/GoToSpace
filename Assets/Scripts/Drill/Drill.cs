using UnityEngine;
using System.Collections;

public class Drill : MonoBehaviour {

    public float intensity = 30.0f;
	public float distance = 25.0f;
	public float overDrillTime = 0.0f;
    private float timeToComplete = 0.0f;

	private bool allComplete = false;

	private TextMesh textMesh;
	private bool didAdvanceStage;
	private bool waiting;
	private float waitTime;
	private string grade;

	// Use this for initialization
	void Start () {
	
		textMesh = GameObject.Find("ScoreText").GetComponent<TextMesh>();
		textMesh.text = "";
		didAdvanceStage = false;
		waiting = true;
		waitTime = 2.0f;
		grade = "F";
	}
	
	// Update is called once per frame
	void Update () {
		checkCompletion ();

		if (allComplete && waiting) {
			AssessGrade();
			DisplayScoreText();
			waiting = false;
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 3.0f;
		gameObject.transform.position = mousePos;

        if (!waiting)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            timeToComplete += Time.deltaTime;
        }
		if (waitTime <= 0 && Input.GetMouseButton(0))
		{
			Advance();
		}
        if (Input.GetMouseButton(0))
        {
            Shake(intensity);
        }
	}

    void Shake(float intensity)
    {
		Vector3 temp = new Vector3(transform.position.x + (Mathf.Sin(Time.time * intensity) / distance), transform.position.y, transform.position.z);
        transform.position = temp;
    }

	void checkCompletion()
	{
		allComplete = true;
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("Point1")) {
			if (!x.GetComponent<Screw>().complete){
				allComplete = false;
			}
		}
	}

	private void AssessGrade()
	{
        if (timeToComplete < 10.5f && overDrillTime < .2f)
        {
            grade = "S";
        }
        else if (timeToComplete < 11.0f && overDrillTime < .25f)
        {
            grade = "A";
        }
        else if (timeToComplete < 12.0f && overDrillTime < .35f)
        {
            grade = "B";
        }
        else if (timeToComplete < 13.0f && overDrillTime < .4f)
        {
            grade = "C";
        }
        else if (timeToComplete < 14.0f && overDrillTime < .45f)
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
		textMesh.text = "End Stage!\n\nTime Overdrilled: " + Mathf.Round(overDrillTime * 100f) / 100f + "\nTotal Time: " + Mathf.Round(timeToComplete * 100f) / 100f + "\n\nGrade: " + grade;
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
