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
	}

	#endregion

	private GameControl gameControl = GameControl.instance;
	public Enemy enemyPrefab;
    public List<Transform> spawnPoints = new List<Transform>(); // enemy spawn points
    private float countdown = 5f; // countdown from round start to enemy spawn

    private bool scan = true; //scan once game starts hopefully

	public delegate void OnWaveReady();
	public OnWaveReady onWaveReadyCallback;
	public delegate void OnWaveStarted();
	public OnWaveStarted onWaveStartedCallback;


	private void Update() {
		if (Input.GetKeyDown("3")) {
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

			if (scan) {
				AstarPath.active.Scan(); //This was needed to resan once the PG is done generating, maybe should change
				scan = false;
			}

			// spawn enemies (placeholder)
			// TODO: Different enemies spawning based on wave
			print(gameControl.GetWaveNumber());
			for (int i = 0; i < gameControl.GetWaveNumber(); i++) {
				SpawnEnemy();
				yield return new WaitForSeconds(0.5f);
			}
		} else {
			Debug.Log("Error: wave started from state other than 0.");
		}
		yield return null;
	}

    void SpawnEnemy() {
		// spawn this enemy at a random spawn point in the list
		int index = (int) Random.Range(0, spawnPoints.Count - 1);
		// instantiate the enemy and add it to the enemy list
		gameControl.AddEnemy(Instantiate(enemyPrefab, spawnPoints[index].position, spawnPoints[index].rotation));
    }

	public void EnemyKilled(Enemy enemy) {
		gameControl.RemoveEnemy(enemy);
		if (gameControl.GetEnemyCount() == 0) {
			gameControl.SetGameState(0); // game state 0 - waiting to start wave
		}
	}
}

