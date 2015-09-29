using UnityEngine;
using System.Collections;

public class GuyBehavior : MonoBehaviour {

    public float leftBound;
    public float rightBound;
	public float walkSpeed;
    public bool isWalking;
    public bool facingRight;

	// Use this for initialization
	void Start () {
        leftBound = transform.position.x - 1.0f;
        rightBound = transform.position.x + 1.3f;
		walkSpeed = .005f;
        isWalking = false;
        facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isWalking)
        {
            if (facingRight)
            {
                Vector3 temp = transform.position;
                temp.x += walkSpeed;
                if (transform.position.x < rightBound)
                {
                    transform.position = temp;
                }
            }
            else
            {
                Vector3 temp = transform.position;
				temp.x -= walkSpeed;
                if (transform.position.x > leftBound)
                {
                    transform.position = temp;
                }
            }

            if (Random.Range(1, 50) == 1) // 1 in 50 chance to stop walking
            {
                isWalking = false;
            }
        }
        else
        {
            if (Random.Range(1, 60) == 1) // 1 in 60 chance to start walking
            {
                isWalking = true;
                if (Random.Range(0, 2) == 1) // Can either start walking left or right
                {
                    facingRight = true;
                }
                else
                {
                    facingRight = false;
                }
            }
        }
	}
}
