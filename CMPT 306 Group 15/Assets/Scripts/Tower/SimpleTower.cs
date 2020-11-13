using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTower : Tower {
	int level = 1;
	public int maxLevel = 5;
	// change in values for each subsequent level
	public float levelup_searchInterval = -0.1f;
	public float levelup_searchRange = -1.0f;
	public float levelup_cooldown = -0.1f;
	public float levelup_damage = 0.1f;
	public float levelup_bulletVelocity = 1.0f;

    void Start()
    {
		base.Start();
    }

	public int GetLevel() {
		return this.level;
	}

	public int GetMaxLevel() {
		return this.maxLevel;
	}

	public void IncreaseLevel() {
		if (this.level == this.maxLevel) return;
		this.level++;
		this.searchInterval += this.levelup_searchInterval;
		this.searchRange += this.levelup_searchRange;
		this.cooldown += this.levelup_cooldown;
		this.damage += this.levelup_damage;
		this.bulletVelocity += this.levelup_bulletVelocity;
	}


}
