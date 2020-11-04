using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 = Down
    //2 = Left
    //3 = Up
    //4 = Right

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //Spawn Bottom
                rand = Random.Range(0, templates.DownRooms.Length);
                Instantiate(templates.DownRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //Spawn Left
                rand = Random.Range(0, templates.LeftRooms.Length);
                Instantiate(templates.LeftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //Spawn Up
                rand = Random.Range(0, templates.UpRooms.Length);
                Instantiate(templates.UpRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //Spawn Right
                rand = Random.Range(0, templates.RightRooms.Length);
                Instantiate(templates.RightRooms[rand], transform.position, Quaternion.identity);
            }
            Instantiate(templates.HideRoom, transform.position, Quaternion.identity);
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (other.CompareTag("Spawn Point") && other.GetComponent<RoomSpawner>() != null)
        {

            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {

                Instantiate(templates.ClosedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
