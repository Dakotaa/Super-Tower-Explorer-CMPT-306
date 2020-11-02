using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint; //change this

    public float timeBetweenWaves = 5f;

    public Text waveCountdownText;

    private float countdown = 2f; //seconds

    private int waveNumber = 0;

    private int maxWaveNumber = 5;

    private void Update()
    {
        if (countdown <= 0f) //if coundown up spawn enemies
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;


        }
        countdown -= Time.deltaTime; //time passed snce last frame(reduce by 1 every second)

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        if (waveNumber != maxWaveNumber)
        {
            Debug.Log("WAVE incoming");
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
            waveNumber++;
        }
        else { //just loop through as a test
            SpawnEnemy();
            waveNumber = 1;
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
       
    }


}

