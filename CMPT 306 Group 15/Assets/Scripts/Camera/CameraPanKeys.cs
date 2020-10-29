using UnityEngine;

public class CameraPanKeys : MonoBehaviour
{
    public float panSpeed = 5f;
    private Vector3 lastPosition;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Finds camera
        cam = Camera.main;
    }
    private void Update()
    {
        // For Keyboard panning
        lastPosition = transform.position;
        if (Input.GetKey("w"))
        {
            lastPosition.y += panSpeed * Time.deltaTime * cam.orthographicSize;
        }
        if (Input.GetKey("s"))
        {
            lastPosition.y -= panSpeed * Time.deltaTime * cam.orthographicSize;
        }
        if (Input.GetKey("a"))
        {
            lastPosition.x -= panSpeed * Time.deltaTime * cam.orthographicSize;
        }
        if (Input.GetKey("d"))
        {
            lastPosition.x += panSpeed * Time.deltaTime * cam.orthographicSize;
        }

        transform.position = lastPosition;
    }
}
