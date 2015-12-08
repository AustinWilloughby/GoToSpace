using UnityEngine;
using System.Collections;

public class NailScript : MonoBehaviour {

    public int hits;
    public int misses;
    float darkness;
    Transform hitbox;
    public Sprite sp1;
    public Sprite sp2;
    public Sprite sp3;
    public Sprite sp4;
	public Sprite bent;

	// Use this for initialization
	void Start () {
        hitbox = gameObject.transform.GetChild(0);
        hits = 0;
        misses = 0;
        darkness = 1.0f;
        GetComponent<SpriteRenderer>().color = new Color(darkness, darkness, darkness, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponentsInChildren<ItemScript>()[1].mousedOver)
        {
            switch (hits)
            {
                case 0:
                    GetComponent<SpriteRenderer>().sprite = sp1;
                    hitbox.transform.position -= Vector3.up * 0.13f;
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().sprite = sp2;
                    hitbox.transform.position -= Vector3.up * 0.16f;
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().sprite = sp3;
                    hitbox.transform.position -= Vector3.up * 0.15f;
                    break;
                case 3:
                    GetComponent<SpriteRenderer>().sprite = sp4;
                    break;
                default:
                    break;
            }
            hits++;
        }

        else if (Input.GetMouseButtonDown(0) && GetComponent<ItemScript>().mousedOver && misses <= 2)
        {
            misses++;
            darkness -= .1f;
            GetComponent<SpriteRenderer>().color = new Color(darkness, darkness, darkness, 1.0f);
        }

		if (misses > 2) {
			darkness = 1.0f;
			GetComponent<SpriteRenderer>().sprite = bent;
		}
	}
}
