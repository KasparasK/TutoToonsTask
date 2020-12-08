using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    public float sceneWidth = 10;

    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        cam.orthographicSize = desiredHalfHeight;
    }
}