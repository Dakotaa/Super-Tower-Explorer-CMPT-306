using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MainController {
	private GameControl gameControl;
	private WaveControl waveControl;

	private void Start() {
		gameControl = GameControl.instance;
		waveControl = WaveControl.instance;
	}

	void Update() {
        if (gameObject.GetComponent<AIPath>().reachedEndOfPath) {
			gameControl.ChangeHealth(-5);
			Kill();
        }
    }

	public void Kill() {
		waveControl.EnemyKilled(this);
		Destroy(this.gameObject);
	}
}
