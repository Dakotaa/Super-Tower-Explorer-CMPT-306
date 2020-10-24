using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventSpawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spawn Point"))
        {
            Destroy(collision.gameObject);
        }
    }
}
