using UnityEngine;
using System.Collections;

public class LaunchScript : MonoBehaviour
{
    public enum launchStage
    {
        Stationary,
        FiringUp,
        LiftOff,
        Cruise
    };

    public GameObject rocket;
    public RocketThruster rocketThrust;
    public float sequenceTime;
    private float sequenceTimer;
    public launchStage currentStage;

    // Use this for initialization
    void Start()
    {
        currentStage = launchStage.Stationary;
        sequenceTimer = sequenceTime;
    }

    // Update is called once per frame
    void Update()
    {
        sequenceTimer -= Time.deltaTime;
        if(sequenceTimer > (sequenceTime - 2))
        {
            currentStage = launchStage.Stationary;
        }
        else if(sequenceTimer > (sequenceTime - 4) && sequenceTimer < (sequenceTime - 2))
        {
            currentStage = launchStage.FiringUp;
            //gameObject.GetComponent<Camera>().orthographicSize -= .01f * Time.deltaTime;
        }
        else if(sequenceTimer > (sequenceTime - 7) && sequenceTimer < (sequenceTime - 4))
        {
            currentStage = launchStage.LiftOff;
        }
        else
        {
            currentStage = launchStage.Cruise;
        }

        Vector3 rocketPos = rocket.transform.position;
        switch(currentStage)
        {
            case launchStage.Stationary:
                rocketThrust.particleInterval = .5f;
                break;


            case launchStage.FiringUp:
                rocketThrust.particleInterval = .1f;
                rocketPos.y += .1f * Time.deltaTime * (10.0f / sequenceTimer);
                rocket.transform.position = rocketPos;

                rocket.transform.Rotate(0, 0, Random.Range(-.25f, .25f));
                break;


            case launchStage.LiftOff:
                rocketThrust.particleInterval = .02f;

                rocketPos.y += 3.0f * Time.deltaTime;
                rocket.transform.position = rocketPos;
                if (rocket.transform.rotation.z < -1.0f || rocket.transform.rotation.z > 359.0f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(0.0f, .3f));
                }
                else if(rocket.transform.rotation.z > 1.0)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-0.3f, 0.0f));
                }
                else
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-.5f, .5f));
                }
                break;


                //Default is standin for cruise
            default:
                if (rocket.transform.rotation.z < - .03f || rocket.transform.rotation.z > 360.0f - .03f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(0.0f, .01f));
                }
                else if (rocket.transform.rotation.z > .03f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-0.01f, 0.0f));
                }
                break;
        }
    }
}

