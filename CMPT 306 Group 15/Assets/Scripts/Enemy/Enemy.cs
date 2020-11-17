using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MainController {
	private GameControl gameControl;
	private WaveControl waveControl;
	private bool isDead;
	public float EXPWorth = 1.0f;

	private void Start() {
		gameControl = GameControl.instance;
		waveControl = WaveControl.instance;
	}

	void Update() {
        if (gameObject.GetComponent<AIPath>().reachedEndOfPath) {
			gameControl.ChangeHealth(-5);
			Kill(false);
        }
    }

	public float GetEXPWorth() {
		return this.EXPWorth;
	}

	public bool IsDead() {
		return this.isDead;
	}

	public void Kill(bool rewardEXP) {
		this.isDead = true;
		gameControl.EnemyKilled(this, rewardEXP);
		Destroy(this.gameObject);
	}
}
