using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {

	public GameObject text;
	Vector3 temp;
	public bool growing;
	public bool shrinking;
	public bool done;
	GameObject guy;
	public float waiting;

	// Use this for initialization
	void Start () {
		guy = GameObject.Find ("Guy");
		growing = false;
		shrinking = false;
		text.GetComponent<TextMesh> ().text = "";
		temp = new Vector3 (.5f, 0, 0);
		transform.localScale = temp;
		done = false;
		waiting = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (waiting > 0.0f && temp.y >= .5f)
		{
			waiting -= Time.deltaTime;
		}
		else if (waiting <= 0.0f)
		{
			if (Input.GetMouseButton(0))
			{
				shrinking = true;
				done = true;
			}
		}

		transform.position = guy.transform.position + new Vector3 (1.9f,1.9f,0);

		// No Craftables on screen and there is still stuff left to craft
		if ((GameObject.FindGameObjectsWithTag("CraftingMat").GetLength(0) == 0 && GameObject.Find ("StatusTracker").GetComponent<StatusTracker>().itemsNeeded.Count > 0) && !done)
			growing = true;

		// Special case for the first stage
		if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Platform && !done)
			growing = true;

		// Special case for the last stage
		if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.ToSpace && !done)
			growing = true;

		// Special case for stages with no minigame
		if (GameObject.Find ("Main Camera").transform.GetChild (0).GetComponent<ScreenFade> ().shouldAdvanceStage && !done)
			growing = true;

		if (temp.y >= .5f) {
			growing = false;
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Platform)
				text.GetComponent<TextMesh>().text = "Let's Go To Space!\nGet the nails,\nwood, and hammer!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Skeleton)
				text.GetComponent<TextMesh>().text = "Let's go find a \nwelding torch!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Interior)
				text.GetComponent<TextMesh>().text = "Let's go find a \nseat!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Interior && GameObject.Find ("Main Camera").transform.GetChild (0).GetComponent<ScreenFade> ().shouldAdvanceStage)
				text.GetComponent<TextMesh>().text = "Let's go find a \ndrill!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingFins)
				text.GetComponent<TextMesh>().text = "Let's go find a \nsaw!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingWindow)
				text.GetComponent<TextMesh>().text = "Let's go find a \nwindow!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.AddingWindow && GameObject.Find ("Main Camera").transform.GetChild (0).GetComponent<ScreenFade> ().shouldAdvanceStage)
				text.GetComponent<TextMesh>().text = "Let's go find \npaint!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.Painting && GameObject.Find ("Main Camera").transform.GetChild (0).GetComponent<ScreenFade> ().shouldAdvanceStage)
				text.GetComponent<TextMesh>().text = "Let's go find \nfuel!";
			if (GameObject.Find("StatusTracker").GetComponent<StatusTracker>().currentStage == CurrentlyBuilding.ToSpace)
				text.GetComponent<TextMesh>().text = "Let's go to space!!";
		}

		if (growing) {
			temp += new Vector3(0, Time.deltaTime / 2.0f, .01f);
			transform.localScale = temp;
		}

		if (shrinking && transform.localScale.y > 0.0f)
		{
			text.GetComponent<TextMesh>().text = " ";
			temp -= new Vector3(0, Time.deltaTime / 2.0f, .01f);
			transform.localScale = temp;
		}

		else if (shrinking)
		{
			shrinking = false;
			transform.localScale = new Vector3(0, 0, 0);
		}
	}
}
