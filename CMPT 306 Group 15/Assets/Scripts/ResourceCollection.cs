using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    private int resourceCounter = 0;

    void OnMouseDown()
    {
        resourceCounter += 1;
    }
}
