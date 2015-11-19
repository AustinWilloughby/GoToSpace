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
    private Animator animator;
    private float behaviorTimer;
    private bool possiblyTurn;
 

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = false;
        possiblyTurn = true;
        facingRight = true;
        WalkingToPosition = false;
        behaviorTimer = Random.Range(1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationHandling();
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
        if (!WalkingToPosition)
        {
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
                newPos.x = transform.position.x + (walkSpeed);
            }
            else
            {
                facingRight = false;
                newPos.x = transform.position.x - (walkSpeed);
            }
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        else
        {
            WalkingToPosition = false;
        }
    }

    private void AnimationHandling()
    {
        if(facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(isWalking)
        {
            animator.CrossFade("GuyWalk", 0.0f);
        }
        else
        {
            animator.CrossFade("GuyIdle", 0.0f);
        }
    }

    //Random walking when not targeted walking
    private void RandomWalking()
    {
        behaviorTimer -= Time.deltaTime;

        //If we need a new behavior
        if(behaviorTimer <= 0)
        {
            possiblyTurn = true;
            isWalking = !isWalking;
            behaviorTimer = Random.Range(1.5f, 4.0f);
        }
        //If idiling and getting close to walking
        else if(possiblyTurn && !isWalking && behaviorTimer > 0 && behaviorTimer < Random.Range(.3f, 1.0f))
        {
            possiblyTurn = false;
            if (Random.Range(0, 2) == 1)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
        }

        //If walking
        if(isWalking)
        {
            Vector3 temp = transform.position;
            if(facingRight)
            {
                temp.x += walkSpeed;
            }
            else
            {
                temp.x -= walkSpeed;
            }

            if(temp.x > rightBound || temp.x < leftBound)
            {
                temp.x = transform.position.x;
                isWalking = false;
                behaviorTimer = Random.Range(.5f, 2.0f);
            }

            transform.position = temp;
        }
    }
}
