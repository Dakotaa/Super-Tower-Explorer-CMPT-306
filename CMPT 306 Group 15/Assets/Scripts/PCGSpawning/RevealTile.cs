using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RevealTile : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;
    public GameObject cell;

    public GameObject mytile;

    private bool rev;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rev = false;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (GameControl.instance.cellUnlockAvailable && GameControl.instance.GetGameState() == 0)
        {
            if (rev)
            {
                // this object was clicked
                Instantiate(cell, transform.position, Quaternion.identity);
                Instantiate(mytile, transform.position, Quaternion.identity);
                Spawn();
                GameControl.instance.LevelComplete();
                Destroy(this.gameObject);
            }
        }
    }

    private void Spawn()
    {
        rand = Random.Range(0, templates.Terrains.Length);
        Instantiate(templates.Terrains[rand], transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("CanOpen"))
        {
            rev = true;
        }
    }
}
