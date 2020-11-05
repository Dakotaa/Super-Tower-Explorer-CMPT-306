using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;



    // Start is called before the first frame update
    void Start()
    {
       templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
       Spawn();
    }

    private void Spawn()
    {
        rand = Random.Range(0, templates.Terrains.Length);
        Instantiate(templates.Terrains[rand], transform.position, Quaternion.identity);
    }
}
