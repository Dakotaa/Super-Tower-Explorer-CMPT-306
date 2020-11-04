using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTowerCreate : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool isTowerDragged = false;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        isTowerDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(27.5f, -27.5f);
    }
}
