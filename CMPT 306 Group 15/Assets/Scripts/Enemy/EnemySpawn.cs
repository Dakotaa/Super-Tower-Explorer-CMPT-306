using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private WaveControl controller;

    private void Start()
    {
        controller = WaveControl.instance;
        if (!controller.spawnPoints.Contains(this.transform))
        {
            controller.spawnPoints.Add(this.transform);
        }
    }

    private void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySpawn"))
        {
            controller.spawnPoints.Remove(other.transform);
            controller.spawnPoints.Remove(this.transform);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
