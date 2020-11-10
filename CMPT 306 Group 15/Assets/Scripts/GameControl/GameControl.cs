using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	/* instance variables */
	public static GameControl instance;

	/* game management variables */
	private bool gameHasEnded = false; //current game state when starting
	public int waveNumber = 0;
	private int gameState = 0; // games state - 0 = waiting to start wave, 1 = wave starting, 2 = wave in progress
	public float restartDelay = 1f;

	/* enemy management variables */
	private List<Enemy> enemies = new List<Enemy>(); // list of active enemies

	/* health management variables */
	public int maxHealth; // maximum (starting) health - unchangeable after start
	private int health; // current health


	private void Update() {
		if (Input.GetKeyDown("2")) {
			ChangeHealth(5);
		}
		if (Input.GetKeyDown("1")) {
			ChangeHealth(-5);
		}
	}


	#region Singleton

	void Awake() {
		/* singleton initialization */
		if (instance != null) {
			Debug.LogWarning("More than one instance of GameControl found!");
			return;
		}
		instance = this;

		this.health = this.maxHealth; // start with full health
	}

	#endregion


	/*
	 * Game management
	 */

	/* handles game ending when health of life tower hits 0 */
	public void EndGame() {
		if (gameHasEnded == false) {
			gameHasEnded = true;
			Debug.Log("Game Over");
			Invoke("Restart", restartDelay);
		}
	}

	void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public int GetGameState() {
		return this.gameState;
	}

	public void SetGameState(int state) {
		if (state >= 0 && state <= 2) {
			this.gameState = state;
		}
	}

	public int GetWaveNumber() {
		return this.waveNumber;
	}

	public void SetWaveNumber(int w) {
		if (w >= 0) {
			this.waveNumber = w;
		}
	}

	public void IncreaseWaveNumber() {
		this.waveNumber++;
	}


   /*
	*  Enemy management
	*/

	public List<Enemy> GetEnemies() {
		return enemies;
	}

	public void AddEnemy(Enemy enemy) {
		enemies.Add(enemy);
	}

	public void RemoveEnemy(Enemy enemy) {
		enemies.Remove(enemy);
	}

	public int GetEnemyCount() {
		return enemies.Count;
	}



	/*
	 *  Health control
	 */

	/* Callback which is triggered on health change. */
	public delegate void OnHealthChanged();
	public OnHealthChanged onHealthChangedCallback;

	public void ChangeHealth(int amt) {
		int newHealth = this.health + amt; // get the new health
		/* avoid going into negative health */
		if (newHealth < 0) {
		/* avoid going over maximum health */
		} else if (newHealth > this.maxHealth) {
			this.health = this.maxHealth;
		} else { 
			this.health = newHealth;
		}

		/* Trigger callback */
		if (onHealthChangedCallback != null) onHealthChangedCallback.Invoke();

		/* end game at 0 health */
		if (this.health == 0) {
			EndGame();
			return;
		}
	}

	public int GetHealth() {
		return this.health;
	}

	public int GetMaxHealth() {
		return this.maxHealth;
	}
}
