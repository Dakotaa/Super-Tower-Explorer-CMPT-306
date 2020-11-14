using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTower : Tower {
	// change in values for each subsequent level
	public float levelup_searchInterval = -0.1f;
	public float levelup_searchRange = +1.0f;
	public float levelup_cooldown = -0.1f;
	public float levelup_damage = +0.1f;
	public float levelup_bulletVelocity = +1.0f;
	public List<Sprite> bodies = new List<Sprite>();
	private SpriteRenderer body;

    public override void Start() {
		base.Start();
		body = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
		body.sprite = bodies[0];
		this.level = 1;
		this.maxLevel = 5;
		this.upgradable = true;
	}

	public override void Update() {
		base.Update();
		if (Input.GetKeyDown("5")) {
			IncreaseLevel();
		}
	}

	public override string GetInfo() {
		string info =	"<b>Level " + this.level + " Tower<b> \n" +
						"Targeting Speed: " + this.searchInterval.ToString("n2") + "s\n" +
						"Targeting Range: " + this.searchRange.ToString("n2") + "\n" +
						"Cooldown: " + this.cooldown.ToString("n2") + "s\n" +
						"Damage: " + this.damage.ToString("n2") + "\n" +
						"Bullet Velocity: " + this.bulletVelocity.ToString("n2");

		if (this.level < this.maxLevel) {
			info += "\n" +
					"<i>Click to upgrade (1 metal)</i>";
		}

		return info;
	}

	public override void IncreaseLevel() {
		if (this.IsMaxLevel()) return;
		this.level++;
		this.searchInterval += this.levelup_searchInterval;
		this.searchRange += this.levelup_searchRange;
		this.cooldown += this.levelup_cooldown;
		this.damage += this.levelup_damage;
		this.bulletVelocity += this.levelup_bulletVelocity;
		body.sprite = bodies[this.level - 1];
		tooltip.Refresh();
	}


}
