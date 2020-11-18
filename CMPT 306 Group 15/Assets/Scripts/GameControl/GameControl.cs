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

	/* experience and cell unclock variables */
	private int level = 0;
	private float EXP = 0;  // player's current EXP towards the next cell unlock
	private float EXPForNextLevel = 5;    // required EXP for the next cell unlock
	private bool cellUnlockAvailable = false;

	private void Update() {
		if (Input.GetKeyDown("2")) {
			ChangeHealth(5);
		}
		if (Input.GetKeyDown("1")) {
			ChangeHealth(-5);
		}

		if (Input.GetKeyDown("3")) {
			ChangeEXP(1);
		}
		if (Input.GetKeyDown("4")) {
			ChangeEXP(-1);
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

	public delegate void OnEnemyDeath();
	public OnEnemyDeath onEnemyDeathCallback;

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

	public void EnemyKilled(Enemy enemy, bool rewardEXP) {
		RemoveEnemy(enemy);
		if (rewardEXP) {
			ChangeEXP(enemy.GetEXPWorth());
		}
		if (onEnemyDeathCallback != null) onEnemyDeathCallback.Invoke();
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
			this.health = 0;
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

	/*
	*  EXP control
	*/

	/* Callback which is triggered on health change. */
	public delegate void OnEXPChanged();
	public OnEXPChanged OnEXPChangedCallback;

	/* change xp amount - analogous to ChangeHealth() */
	public void ChangeEXP(float amt) {
		/* don't increase when an unlock is already available */
		if (cellUnlockAvailable) {
			return;
		}
		float newEXP = this.EXP + amt;
		if (newEXP < 0) {
			this.EXP = 0;	
		} else if (newEXP > this.EXPForNextLevel) {
			this.EXP = EXPForNextLevel;
		} else {
			this.EXP = newEXP;
		}
		if (OnEXPChangedCallback != null) OnEXPChangedCallback.Invoke();
		if (this.EXP >= this.EXPForNextLevel) {
			LevelComplete();
		}
	}

	public float GetEXP() {
		return this.EXP;
	}

	public float GetNextLevelEXP() {
		return this.EXPForNextLevel;
	}

	private void LevelComplete() {
		this.level++;
		//this.cellUnlockAvailable = true;
		this.EXP = 0;
		this.EXPForNextLevel = CalcNextLevel();
		if (OnEXPChangedCallback != null) OnEXPChangedCallback.Invoke();
	}

	/*
	 * Calculates the EXP needed for the next level.
	 */
	private float CalcNextLevel() {
		return this.EXPForNextLevel * 2.0f;
	}
}
