using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint; //change this

    public float timeBetweenWaves = 5f;

    private float countdown = 2f; //seconds

    private int waveNumber = 1;

    private void Update()
    {
        if (countdown <= 0f) //if coundown up spawn enemies
        {
            SpawnWave();
            countdown = timeBetweenWaves;

        }
        countdown -= Time.deltaTime; //time passed snce last frame(reduce by 1 every second)
    }

    void SpawnWave()
    {
        Debug.Log("WAVE incoming");
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }


}

