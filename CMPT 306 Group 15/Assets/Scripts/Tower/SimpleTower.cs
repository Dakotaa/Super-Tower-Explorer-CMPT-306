﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTower : Tower {
	private int level = 1;
	private int maxLevel = 5;
	// change in values for each subsequent level
	public float levelup_searchInterval = -0.1f;
	public float levelup_searchRange = 1.0f;
	public float levelup_cooldown = -0.1f;
	public float levelup_damage = 0.1f;
	public float levelup_bulletVelocity = 1.0f;
	public List<Sprite> bodies = new List<Sprite>();
	private SpriteRenderer body;

    void Start() {
		base.Start();
		body = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
		body.sprite = bodies[0];
    }

	public void Update() {
		base.Update();
		if (Input.GetKeyDown("5")) {
			IncreaseLevel();
		}
	}

	public int GetLevel() {
		return this.level;
	}

	public int GetMaxLevel() {
		return this.maxLevel;
	}

	public void IncreaseLevel() {
		if (this.level >= this.maxLevel) return;
		this.level++;
		print("Now level " + this.level);
		this.searchInterval += this.levelup_searchInterval;
		this.searchRange += this.levelup_searchRange;
		this.cooldown += this.levelup_cooldown;
		this.damage += this.levelup_damage;
		this.bulletVelocity += this.levelup_bulletVelocity;
		body.sprite = bodies[this.level - 1];
	}


}
