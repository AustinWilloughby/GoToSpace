using UnityEngine;

public class CameraScale : MonoBehaviour
{
    //Sets the scaling resolution
    public float width = 16f;
    public float height = 9f;

    void Awake()
    {
        //Scales the camera aspect ratio to the resolution
        //Effectivly forcing the camera to scale the game to the device size
        //While maintaining the default drawing ratio
        Camera.main.aspect = width / height;
    }
}