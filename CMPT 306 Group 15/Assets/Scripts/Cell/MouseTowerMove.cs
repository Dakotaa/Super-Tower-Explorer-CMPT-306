using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTowerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
        }
    }*/

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;
    }
}
