﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*// Class used to move the tower picture in build menu around on screen by dragging
public class MouseTowerCreate : MonoBehaviour, IDragHandler, IEndDragHandler

{
    public bool isTowerDragged = false;
    public int resourceCost = 1;

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
}*/

// Class used to move the tower picture in build menu around on screen by clicking
public class MouseTowerCreate : MonoBehaviour, IPointerClickHandler
{
    public bool isTowerDragged = false;
    public int resourceCost = 1;
    private Vector3 resetPosition = new Vector3(27.5f, -27.5f);

    // Moves the tower picture when mouse button clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isTowerDragged)
        {
            isTowerDragged = true;
        }
        else
        {
            isTowerDragged = false;
        }
    }

    // Moves the tower picture when mouse button clicked
    public void Update()
    {
        if (isTowerDragged)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            transform.localPosition = resetPosition;
        }
    }
}