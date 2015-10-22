using UnityEngine;
using System.Collections;

public class NailGameController : MonoBehaviour {

    public bool completed;
    public bool failed;
    public GameObject nail1;
    public GameObject nail2;
    public GameObject nail3;
    public GameObject nail4;

    private MusicSingleton musicHandler;

	// Use this for initialization
	void Start () {
        completed = false;
        failed = false;
        musicHandler = MusicSingleton.Instance;
        musicHandler.SetCurrentMusic(1);
	}
	
	// Update is called once per frame
	void Update () {
        if (nail1.GetComponent<NailScript>().hits > 3 &&
            nail2.GetComponent<NailScript>().hits > 3 &&
            nail3.GetComponent<NailScript>().hits > 3 &&
            nail4.GetComponent<NailScript>().hits > 3)
        {
            completed = true;
            Application.LoadLevel("Workshop");
        }

        if (nail1.GetComponent<NailScript>().misses > 4 ||
            nail2.GetComponent<NailScript>().misses > 4 ||
            nail3.GetComponent<NailScript>().misses > 4 ||
            nail4.GetComponent<NailScript>().misses > 4)
        {
            failed = true;
        }
	}
}
