using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawning : MonoBehaviour
{
    public GameObject cell;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cell, transform.position, Quaternion.identity);
    }
}
