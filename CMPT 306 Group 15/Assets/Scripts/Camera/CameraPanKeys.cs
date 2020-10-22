using UnityEngine;

public class CameraPanKeys : MonoBehaviour
{
    public float panSpeed = 5f;
    private Vector3 lastPosition;

    private void Update()
    {
        // For Keyboard panning
        lastPosition = transform.position;
        if (Input.GetKey("w"))
        {
            lastPosition.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            lastPosition.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            lastPosition.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            lastPosition.x += panSpeed * Time.deltaTime;
        }

        transform.position = lastPosition;
    }
}
