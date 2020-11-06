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
	private GameControl gameControl;

	private void Update() {
		if (Input.GetKeyDown("1")) {
			DecreaseHealth(5);
		}
	}

	void Start() {
		gameControl = FindObjectOfType<GameControl>();
		this.bar = GameObject.Find("Health Bar").GetComponent<RectTransform>();
		this.text = GameObject.Find("Health Text").GetComponent<Text>();
		this.health = this.baseHealth;
		this.baseWidth = this.bar.rect.width;
		this.width = this.baseWidth;
	}

	public void DecreaseHealth(int amt) {
		this.health -= amt;	// update health
		this.text.text = "HP: " + this.health + "/" + this.maxHealth;	// update health text
		float newWidth = this.baseWidth * ((float) this.health / (float) this.maxHealth);   // get new width
		StartCoroutine(DecreaseBar(newWidth));	// animate health decrease

		if (this.health - amt <= 0) {   // check if new health <= 0
			gameControl.EndGame();
			return;
		}
	}

	private IEnumerator DecreaseBar(float newWidth) {
		int frames = 30; // the number of frames to animate health bar decrease. Greater number means smoother animation
		float diff = (this.width - newWidth) / (float) frames; // the amount to decrease the bar by each frame
		float w;
		for (int i = 1; i < frames + 1; i++) {
			w = this.width - (diff * i);
			this.bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);  // reduce width of bar
			yield return null;
		}
		this.width = newWidth; // update newWidth once animation is complete
		yield return null;
	}
}
