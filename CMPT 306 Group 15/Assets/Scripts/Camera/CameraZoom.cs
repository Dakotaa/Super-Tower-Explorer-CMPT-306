using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float size;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Finds camera and its size
        cam = Camera.main;
        size = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Finds which direction the scrollwheel went and
        // zooms in or out based on it
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        size -= scroll * zoomFactor;
        size = Mathf.Clamp(size, 4.5f, 8f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.deltaTime * zoomLerpSpeed);

        // Old version
        /*float newz = cam.orthographicSize;
        if (scroll > 0f) // Forwards
        {
            if (newz > 4f)
            {
                newz--;
            }
            cam.orthographicSize = newz;
        }
        else if (scroll < 0f) // Backwards
        {
            newz++;
            cam.orthographicSize = newz;
        }*/
    }
}
