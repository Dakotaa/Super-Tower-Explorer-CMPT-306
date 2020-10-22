using UnityEngine;

public class CameraPanMouse : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    private Vector3 lastPosition;

    private void Update()
    {
        // For Mouse Panning
        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }
}
