using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class used to move the tower picture in build menu around on screen
public class MouseTowerCreate : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool isTowerDragged = false;

    // Moves the tower picture when mouse button held down
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        isTowerDragged = true;
    }

    // Moves tower picture back to build menu
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(27.5f, -27.5f);
    }
}
