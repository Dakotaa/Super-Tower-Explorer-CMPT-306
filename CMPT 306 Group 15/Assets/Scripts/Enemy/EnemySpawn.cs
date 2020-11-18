using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool can_spawn = false;

    private WaveControl controller;

    private void Start()
    {
        controller = WaveControl.instance;
    }

    private void Update()
    {
        if (can_spawn)
        {
            controller.spawnPoints.Add(this.transform);
        }
        else if (!can_spawn && controller.spawnPoints.Contains(this.transform))
        {
            controller.spawnPoints.Remove(this.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySpawn"))
        {
            print("hiiii");
            Destroy(other);
            Destroy(this);
        }

        else if (other.CompareTag("HideTile"))
        {
            can_spawn = true;
        }
        else
        {
            can_spawn = false;
        }
    }
}
