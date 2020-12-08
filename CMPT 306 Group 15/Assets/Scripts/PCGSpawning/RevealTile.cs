using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RevealTile : MonoBehaviour
{
    private RoomTemplates templates;
    private int rand;
    public GameObject cell;
    SpriteRenderer sprite;
    bool down = false;
    byte val = 34;
    int count = 0;

    public GameObject mytile;

    private bool rev;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        sprite = GetComponent<SpriteRenderer>();
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

    private void Update()
    {
        if (GameControl.instance.cellUnlockAvailable && GameControl.instance.GetGameState() == 0 && rev)
        {
            if (!down)
            {
                val++;
                sprite.color = new Color32(val, val, val, 255);
                count++;
                if (count == 150)
                {
                    down = true;
                }
            }
            else
            {
                val--;
                sprite.color = new Color32(val, val, val, 255);
                count--;
                if (count == 0)
                {
                    down = false;
                }
            }

        }
        else
        {
            sprite.color = new Color32(34, 34, 34, 255);
        }
    }
}
