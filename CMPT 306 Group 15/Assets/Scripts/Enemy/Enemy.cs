using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MainController
{
	private HealthBar healthBar;
	private GameControl gameControl;

	private void Start() {
		gameControl = GameControl.instance;
		healthBar = FindObjectOfType<HealthBar>();
	}

	void Update() {
        if (gameObject.GetComponent<AIPath>().reachedEndOfPath) {
			gameControl.ChangeHealth(-5);
            Enemy.Destroy(this.gameObject);
        }
    }

	public void Kill() {
		Destroy(this.gameObject);
	}
}
