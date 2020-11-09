using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{

	#region Singleton

	public static WaveManager instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of WaveManager found!");
			return;
		}
		instance = this;
	}

	#endregion

	public Transform enemyPrefab;

    public Transform spawnPoint; //change this

    public float timeBetweenWaves = 10.0f;

    public Text waveCountdownText;

    private float countdown = 2f; //seconds

    private int waveNumber = 0;

    private int maxWaveNumber = 10;

    private bool scan = true; //scan once game starts hopefully

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

	public delegate void OnWaveStarted();
	public OnWaveStarted onWaveStartedCallback;

	IEnumerator SpawnWave()
    {
		// Trigger callback
		if (onWaveStartedCallback != null) onWaveStartedCallback.Invoke();
		if (scan)
        {
            AstarPath.active.Scan();//This was needed to resan once the PG is done generating, maybe should change
            Debug.Log("Scanned");
            scan = false;
        }
        

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

	public int GetWaveNumber() {
		return this.waveNumber;
	}

}

