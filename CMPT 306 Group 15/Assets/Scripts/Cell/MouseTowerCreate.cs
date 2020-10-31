using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTowerCreate : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject obj = null;
    //public CellGrid cellGrid;

    void OnMouseDown()
    {
        obj = (GameObject)Instantiate(Resources.Load("Tower", typeof(GameObject)));
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        // transform.position = curPosition;
        obj.transform.position = curPosition;
    }

    void OnMouseDrag()
    {
        if (obj != null)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            obj.transform.position = curPosition;
        }
    }

    /*private void OnMouseUp()
    {
        Destroy(obj);
        obj = null;
        cellGrid.OnMouseUp();
    }*/
}
