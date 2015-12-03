﻿using UnityEngine;
using System.Collections;

public class FuelTankScript : MonoBehaviour {

    public Sprite deathSprite;
    public int intakeCount;
    GameObject deathScreen;
    private bool dead;

    private TextMesh textMesh;
    private bool didAdvanceStage;
    private bool waiting;
    private float waitTime;
    private string grade;

    private float timeToComplete;

	// Use this for initialization
	void Start () {
        intakeCount = 0;
        deathScreen = GameObject.Find("ded");
        dead = false;

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
        else
        {
            timeToComplete += Time.deltaTime;
        }

        if (waitTime <= 0 && Input.GetMouseButton(0))
        {
            Advance();
        }

        if (GameObject.FindGameObjectWithTag("fuel") == null || timeToComplete > 60.0f || (timeToComplete > 40.0f && dead))
        {
            AssessGrade();
            DisplayScoreText();
            waiting = false;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "fuel" && !coll.gameObject.GetComponent<FuelScript>().hasCollided)
        {
            coll.gameObject.GetComponent<FuelScript>().hasCollided = true;
            intakeCount += 1;
            if (intakeCount > 15)
            {
                dead = true;
                deathScreen.GetComponent<SpriteRenderer>().sprite = deathSprite;
            }
            coll.transform.gameObject.GetComponent<FuelScript>().destroyTrigger = true;
        }
    }

    private void AssessGrade()
    {
        if (timeToComplete <= 14.5f)
        {
            grade = "S";
        }
        else if (timeToComplete <= 18.0f)
        {
            grade = "A";
        }
        else if (timeToComplete <= 22.0f)
        {
            grade = "B";
        }
        else if (timeToComplete <= 26.0f)
        {
            grade = "C";
        }
        else if (timeToComplete <= 30.0f)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }

        if (dead)
        {
            grade = "F";
        }
    }

    private void DisplayScoreText()
    {
        GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().fadeOut = true;
		if (dead) {
			textMesh.text = "RIP";
		}
		else if (timeToComplete < 60.0f) 
		{
			textMesh.text = "End Stage!\n\nTime: " + Mathf.Round (timeToComplete * 100f) / 100f + "\n\nGrade: " + grade;
		} 
		else 
		{
			textMesh.text = "Time Up!\n\nTime: " + Mathf.Round (timeToComplete * 100f) / 100f + "\n\nGrade: " + grade;
		}
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
