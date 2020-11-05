using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealTile : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;
    public GameObject cell;
    public GameObject up;
    public GameObject right;
    public GameObject down;
    public GameObject left;
    public GameObject reveal;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    void OnMouseDown()
    {
        if (true || up.GetComponent<IsHidden>().revealed ||
            right.GetComponent<IsHidden>().revealed ||
            down.GetComponent<IsHidden>().revealed ||
            left.GetComponent<IsHidden>().revealed)
        {
            // this object was clicked
            Instantiate(cell, transform.position, Quaternion.identity);
            Instantiate(reveal, transform.position, Quaternion.identity);
            Spawn();
            Destroy(this.gameObject);

        }

    }

    private void Spawn()
    {
        rand = Random.Range(0, templates.Terrains.Length);
        Instantiate(templates.Terrains[rand], transform.position, Quaternion.identity);
    }

}
