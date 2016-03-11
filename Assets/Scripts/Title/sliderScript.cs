using UnityEngine;
using System.Collections;

public class sliderScript : MonoBehaviour
{
    public float leftBound;
    public float rightBound;
    private float value;
    private GameObject target;

    Vector3 lastMousePos;
    // Use this for initialization
    void Start()
    {
        if (gameObject.name == "musicSlider")
        {
            target = GameObject.Find("MusicHandler");
        }
        else
        {
            target = GameObject.Find("AmbientSounds");
        }
    }

    // Update is called once per frame
    void Update()
    {
        value = (transform.localPosition.x + Mathf.Abs(leftBound)) / (Mathf.Abs(leftBound) + Mathf.Abs(rightBound));
        if (gameObject.name == "musicSlider")
        {
            target.GetComponent<MusicSingleton>().volume = value;
        }
        else
        {
            target.GetComponent<AmbienceSingleton>().volume = value;
        }
    }

    void OnMouseDown()
    {
        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        mousePos.y = transform.position.y;

        Vector3 move = gameObject.transform.localPosition;

        move.x += mousePos.x - lastMousePos.x;
        if (move.x < leftBound) { move.x = leftBound; }
        if (move.x > rightBound) { move.x = rightBound; }
        transform.localPosition = move;

        lastMousePos = mousePos;
    }
}
