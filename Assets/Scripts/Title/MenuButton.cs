using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("OptionsButton").GetComponent<OptionsHandler>().DoMenuButton();
        }
    }
}
