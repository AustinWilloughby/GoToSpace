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
    public AudioSource soundEffect;
    public MusicSingleton musicSingleton;
    private float restoreVolume;
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
        if(musicSingleton == null)
        {
            if(MusicSingleton.Instance != null)
            {
                musicSingleton = MusicSingleton.Instance;
                restoreVolume = musicSingleton.volume;
            }
        }
        else
        {
            if(sequenceTimer < sequenceTime)
            {
                musicSingleton.volume -= .5f * Time.deltaTime;
            }
            else if (sequenceTimer <= 0)
            {
                musicSingleton.volume = restoreVolume;
            }
        }

        sequenceTimer -= Time.deltaTime;
        if (sequenceTimer < sequenceTime - 1)
        {
            if (!soundEffect.isPlaying && sequenceTimer > 10)
            {
                soundEffect.Play();
            }
        }

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

                rocketPos.y += 4.0f * Time.deltaTime;
                rocket.transform.position = rocketPos;
                if (rocket.transform.rotation.z < -1.0f || rocket.transform.rotation.z > 359.0f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(0.0f, .15f));
                }
                else if(rocket.transform.rotation.z > 1.0)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-0.15f, 0.0f));
                }
                else
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-.3f, .3f));
                }
                break;


                //Default is standin for cruise
            default:
                rocketThrust.particleInterval = 0.001f;

                rocketPos.y += 3.5f * Time.deltaTime;
                rocket.transform.position = rocketPos;
                if (rocket.transform.rotation.z < - .03f || rocket.transform.rotation.z > 360.0f - .03f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(0.0f, .03f));
                }
                else if (rocket.transform.rotation.z > .03f)
                {
                    rocket.transform.Rotate(0, 0, Random.Range(-0.03f, 0.0f));
                }
                else
                {
                    rocket.transform.rotation.SetEulerAngles(0, 0, 0);
                }
                break;
        }

        if(rocket.transform.position.y > transform.position.y && transform.position.y < 35.0f)
        {
            Vector3 cameraPos = transform.position;
            cameraPos.y = rocket.transform.position.y;
            if(cameraPos.x < rocket.transform.position.x)
            {
                cameraPos.x += 2 * Time.deltaTime;
            }
            transform.position = cameraPos;
        }
        else if(transform.position.y > 35.0f)
        {
            Color black = GetComponentInChildren<SpriteRenderer>().color;
            black.a += .28f * Time.deltaTime;
            GetComponentInChildren<SpriteRenderer>().color = black;

            if(black.a >= 1)
            {
                musicSingleton.audioPlayer.Stop();
                musicSingleton.volume = restoreVolume;
                Application.LoadLevel("InsideShip");
            }
        }
    }
}

