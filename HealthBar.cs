using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	public int baseHealth; // starting health, adjustable
	public int maxHealth;  // max health, adjustable
	private int health; // current health
	private float baseWidth; // starting width of the health bar image
	private float width; // current width
	private RectTransform bar;	// red bar image
	private Text text;  // HP display text

	private void Update() {
		if (Input.GetKeyDown("1")) {
			DecreaseHealth(5);
		}
	}

	void Start() {
		this.bar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
		this.text = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
		this.health = this.baseHealth;
		this.baseWidth = this.bar.rect.width;
		this.width = this.baseWidth;
	}

	public void DecreaseHealth(int amt) {
		if (this.health - amt <= 0) {   // check if new health <= 0
			FindObjectOfType<GameControl>().EndGame();
		}
		this.health -= amt;	// update health
		this.text.text = "HP: " + this.health + "/" + this.maxHealth;	// update health text
		this.width = this.baseWidth * ((float) this.health / (float) this.maxHealth);	// get new width
		this.bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.width);	// reduce width of bar
	}
}
