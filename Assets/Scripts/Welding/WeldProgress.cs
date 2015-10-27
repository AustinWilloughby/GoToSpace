using UnityEngine;
using System.Collections;

public class WeldProgress : MonoBehaviour {

	GameObject[] weldTargets;
	public int completed;
	public int failed;

	// Use this for initialization
	void Start () {
		weldTargets = GameObject.FindGameObjectsWithTag ("WeldTarget");
		completed = 0;
		failed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		checkProgress ();
	}

	void checkProgress()
	{
		completed = 0;
		failed = 0;
		for (int i = 0; i < weldTargets.Length; i++) 
		{
			if (weldTargets[i].GetComponent<WeldTarget>().completed)
			{
				completed++;
			}

			if (weldTargets[i].GetComponent<WeldTarget>().failed)
			{
				failed++;
			}
		}
	}
}
