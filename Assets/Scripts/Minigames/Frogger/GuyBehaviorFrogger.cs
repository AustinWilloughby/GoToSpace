using UnityEngine;
using System.Collections;

public class GuyBehaviorFrogger : MonoBehaviour
{

    public float leftBound;
    public float rightBound;
    public float walkSpeed;
    private float defaultWalkSpeed;
    public bool isWalking;
    public bool facingRight;
    public int starsLeft;

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
        defaultWalkSpeed = walkSpeed;

        starsLeft = 4;

        //This if statement and its contents control the guy reentering specific scenes at appropriate locations
        //For example, he will be standing in front of the shop he just left when returning to the street scene.
        if(GameObject.Find("StatusTracker"))
        {
            StatusTracker status = StatusTracker.Instance;
            Vector3 transform = gameObject.transform.position;
            switch(status.ThisLevel)
            {
                case 3: //Street
                    switch (status.LastLevelInt)
                    {
                        case 5: //Paint Shop
                            transform.x = -4.47f;
                            transform.y = 0.272f;
                            break;
                        case 6: //Hardware Shop
                            transform.x = 4.22f;
                            transform.y = 0.272f;
                            break;
                        default: //Yard and anywhere else (shouldnt happen)
                            transform.x = 0.182f;
                            transform.y = 0.272f;
                            break;
                    }
                    break;

                case 10: //Yard
                    switch (status.LastLevelInt)
                    {
                        case 3: //Street
                            transform.x = 10.56f;
                            transform.y = -2.93f;
                            break;

                        default: //Workshop and anywhere else (shouldnt happen)
                            transform.x = 1.77f;
                            transform.y = -2.93f;
                            break;
                    }
                    break;

                default: break;
            }
            gameObject.transform.position = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimationHandling();
        if (WalkingToPosition)
        {
            WalkToPosition();
            if (Mathf.Abs(targetPosition.x - transform.position.x) < .05f)
            {
                WalkingToPosition = false;
                walkSpeed = defaultWalkSpeed;
                isWalking = false;
            }
        }

        if (starsLeft == 0)
        {
            GameObject.Find("ScreenBlack").GetComponent<ScreenFade>().fadeOut = true;
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
        if (Mathf.Abs(transform.position.x - targetPosition.x) > .05f || Mathf.Abs(transform.position.y - targetPosition.y) > .05f)
        {
            if (transform.position.x < targetPosition.x)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
            isWalking = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed);
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

    public void StandStill(float time)
    {
        behaviorTimer = time;
        isWalking = false;
    }
}
