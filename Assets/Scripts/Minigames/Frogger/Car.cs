using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour {

    private float lifeSpan = 12.0f;
    public bool movingRight = true;
    private GameObject guy;

    // Use this for initialization
    void Start () {
        guy = GameObject.Find("Guy");
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector2.Distance(gameObject.transform.position, guy.transform.position) < 2.0f)
        {
            GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<ScreenFade>().FadeOut(SceneManager.GetActiveScene().name);
        }

        if (movingRight)
        {
            transform.Translate(Vector3.right * .1f);
        }
        else
        {
            transform.Translate(Vector3.left * .1f);
        }

        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
