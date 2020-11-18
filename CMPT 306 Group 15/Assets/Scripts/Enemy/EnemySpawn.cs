using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private WaveControl controller;

    private void Start()
    {
        controller = WaveControl.instance;
    }

    private void Update()
    {
        if (!controller.spawnPoints.Contains(this.transform))
        {
            controller.spawnPoints.Add(this.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemySpawn"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
