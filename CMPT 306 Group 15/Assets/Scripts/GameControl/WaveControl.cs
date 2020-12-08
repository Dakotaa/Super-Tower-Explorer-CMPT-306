using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveControl : MonoBehaviour
{

	#region Singleton

	public static WaveControl instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of WaveManager found!");
			return;
		}
		instance = this;
		gameControl = GameControl.instance;
		gameControl.onEnemyDeathCallback += EnemyKilled;
	}

	#endregion

	public GameControl gameControl = GameControl.instance;

	public List<Enemy> easyEnemies;
	public List<Enemy> mediumEnemies;
	public List<Enemy> hardEnemies;
	
	public List<Transform> spawnPoints = new List<Transform>(); // enemy spawn points
    public int countdown = 5; // countdown from round start to enemy spawn
	public float enemySpawnInterval = 0.5f;
	public int mediumEnemyDivisor = 5;
	public int hardEnemyDivisor = 20;
	private int waveEnemyCount; // the total overall enemy count for this wave
	private int waveEnemiesSpawned; // the number of enemies spawned so far this wave

	private bool scan = true; //scan once game starts hopefully

	public delegate void OnWaveReady();
	public OnWaveReady onWaveReadyCallback;
	public delegate void OnWaveStarted();
	public OnWaveStarted onWaveStartedCallback;
	public delegate void OnWaveFinished();
	public OnWaveFinished onWaveFinishedCallback;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			StartCoroutine(StartWave());
		}
	}


	// begin a wave. increase wave number, call callbacks, spawn enemies
	IEnumerator StartWave() {
		if (gameControl.GetGameState() == 0) {
			gameControl.IncreaseWaveNumber();
			gameControl.SetGameState(1); // game state 1 - wave about to start
			// Trigger callback to start countdown
			if (onWaveReadyCallback != null) onWaveReadyCallback.Invoke();
			yield return new WaitForSeconds(countdown);
			gameControl.SetGameState(2); // game state 2 - wave active
			// Trigger callback to display wave number
			if (onWaveStartedCallback != null) onWaveStartedCallback.Invoke();
			//This was needed to resan once the PG is done generating, maybe should change
			if (scan) {
				AstarPath.active.Scan();
				scan = false;
			}

			waveEnemyCount = gameControl.GetWaveNumber() * gameControl.GetWaveNumber();
			waveEnemiesSpawned = 0;
			int waveMediumEnemyCount = waveEnemyCount / this.mediumEnemyDivisor;
			int waveHardEnemyCount = waveEnemyCount / this.hardEnemyDivisor;
			int hardEnemiesSpawned = 0;
			int mediumEnemiesSpawned = 0;
			for (int i = 0; i < waveEnemyCount; i++) {
				print(waveMediumEnemyCount);
				print(waveHardEnemyCount);
				Enemy enemy;
				if (hardEnemiesSpawned < waveHardEnemyCount) {
					int index = (int)Random.Range(0, hardEnemies.Count);
					enemy = hardEnemies[index];
					hardEnemiesSpawned++;
				} else if (mediumEnemiesSpawned < waveMediumEnemyCount) {
					int index = (int)Random.Range(0, mediumEnemies.Count);
					enemy = mediumEnemies[index];
					mediumEnemiesSpawned++;
				} else {
					int index = (int)Random.Range(0, easyEnemies.Count);
					enemy = easyEnemies[index];
				}
				SpawnEnemy(enemy);
				yield return new WaitForSeconds(enemySpawnInterval / (float) spawnPoints.Count);
			}

		} else {
			Debug.Log("Error: wave started from state other than 0.");
		}
		yield return null;
	}

	public void NextWave() {
		StartCoroutine(StartWave());
	}

	private void FinishWave() {
		gameControl.SetGameState(0); // game state 0 - waiting to start wave
		if (onWaveFinishedCallback != null) onWaveFinishedCallback.Invoke();
	}

    private void SpawnEnemy(Enemy enemy) {
		int index = (int) Random.Range(0, spawnPoints.Count);
		gameControl.AddEnemy(Instantiate(enemy, spawnPoints[index].position, spawnPoints[index].rotation));
		waveEnemiesSpawned++;
	}

	private void EnemyKilled() {
		// check if this was the last enemy for the round 
		if (gameControl.GetEnemyCount() == 0 && waveEnemyCount == waveEnemiesSpawned) {
			FinishWave();
		}
	}

	public int GetCountdown() {
		return this.countdown;
	}
}

