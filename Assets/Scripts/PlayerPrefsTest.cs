using UnityEngine;
using System.Collections;

public class PlayerPrefsTest : MonoBehaviour {

	public GameObject item01;
	public GameObject item02;
	public GameObject item03;

	// Use this for initialization
	void Start () {
		Vector3 temp = new Vector3 (PlayerPrefs.GetFloat ("item01x"), PlayerPrefs.GetFloat ("item01y"), -1.5f);
		item01.transform.position = temp;

				temp = new Vector3 (PlayerPrefs.GetFloat ("item02x"), PlayerPrefs.GetFloat ("item02y"), -1.5f);
		item02.transform.position = temp;

				temp = new Vector3 (PlayerPrefs.GetFloat ("item03x"), PlayerPrefs.GetFloat ("item03y"), -1.5f);
		item03.transform.position = temp;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat("item01x", item01.transform.position.x);
		PlayerPrefs.SetFloat("item01y", item01.transform.position.y);

		PlayerPrefs.SetFloat("item02x", item02.transform.position.x);
		PlayerPrefs.SetFloat("item02y", item02.transform.position.y);

		PlayerPrefs.SetFloat("item03x", item03.transform.position.x);
		PlayerPrefs.SetFloat("item03y", item03.transform.position.y);
	}
}
