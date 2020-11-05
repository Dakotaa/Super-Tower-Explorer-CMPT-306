using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealTile : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;
    public GameObject cell;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // this object was clicked
        Destroy(this.gameObject);
        Instantiate(cell, transform.position, Quaternion.identity);
        Spawn();
    }

    private void Spawn()
    {
        rand = Random.Range(0, templates.Terrains.Length);
        Instantiate(templates.Terrains[rand], transform.position, Quaternion.identity);
    }

}
