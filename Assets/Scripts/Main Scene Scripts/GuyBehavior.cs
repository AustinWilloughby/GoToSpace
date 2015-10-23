using UnityEngine;
using System.Collections;

public class GuyBehavior : MonoBehaviour
{

    public float leftBound;
    public float rightBound;
    public float walkSpeed;
    public bool isWalking;
    public bool facingRight;

    public bool WalkingToPosition;
    private Vector2 targetPosition;
    private float walkTimer;

    // Use this for initialization
    void Start()
    {
        isWalking = false;
        facingRight = true;
        WalkingToPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!WalkingToPosition)
        {
            RandomWalking();
        }
        else
        {
            WalkToPosition();
            if (Mathf.Abs(targetPosition.x - transform.position.x) < .05f)
            {
                WalkingToPosition = false;
                
            }
        }
    }

    //Initializes the guy walking to a position
    public void MakeWalkToPosition(Vector3 targetPos)
    {
        print("trying");
        if (!WalkingToPosition)
        {
            print("walking");
            WalkingToPosition = true;
            targetPosition = (Vector2)targetPos;
        }
    }

    //Handles moving the guy to target position
    private void WalkToPosition()
    {
        if (Mathf.Abs(transform.position.x - targetPosition.x) > .02f)
        {
            isWalking = true;
            Vector3 newPos;
            newPos.x = 0;
            if (transform.position.x < targetPosition.x)
            {
                facingRight = true;
                print(walkSpeed);
                newPos.x = transform.position.x + (walkSpeed);
            }
            else
            {
                facingRight = false;
                newPos.x = transform.position.x - (walkSpeed);
            }
            walkTimer += Time.deltaTime;
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        else
        {
            WalkingToPosition = false;
        }
    }

    //Random walking when not targeted walking
    private void RandomWalking()
    {
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
