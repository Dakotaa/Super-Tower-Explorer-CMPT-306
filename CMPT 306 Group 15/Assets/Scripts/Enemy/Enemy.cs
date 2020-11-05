using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MainController
{
	private HealthBar healthBar;

	private void Start() {
		healthBar = FindObjectOfType<HealthBar>();
	}

	void Update() {
        if (gameObject.GetComponent<AIPath>().reachedEndOfPath) {
			healthBar.DecreaseHealth(5);
            Enemy.Destroy(this.gameObject);
        }
    }

	public void Kill() {
		Destroy(this.gameObject);
	}
}
