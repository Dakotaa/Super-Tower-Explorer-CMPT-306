using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : MonoBehaviour
{
    private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Input.mousePosition;
        pos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        pos.x = Mathf.Lerp(transform.position.x, (float)RectTransform.Axis.Horizontal + 50f, 1.3f * Time.deltaTime);
        pos.y = Mathf.Lerp(transform.position.y, (float)RectTransform.Axis.Vertical + 50f, 1.3f * Time.deltaTime);
        transform.position = pos;
        if ((transform.position.x < 100f) && (transform.position.y < 120f))
        {
            Destroy(gameObject);
        }
    }
}
