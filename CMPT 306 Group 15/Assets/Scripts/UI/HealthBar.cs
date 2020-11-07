using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles the visual component of the player's health.
 * This includes the health bar and text.
 * Callback functions depend on an instane of GameControl.
 */

public class HealthBar : MonoBehaviour {
	private int maxHealth;  // max health
	private int health; // current health
	private float baseWidth; // starting width of the health bar image
	private float width; // current width
	private RectTransform bar;	// red bar image
	private Text text;  // HP display text
	private GameObject textPanel;
	private GameControl gameControl; // Game Control instance

	void Start() {
		/* game control setup */
		gameControl = GameControl.instance; // grab the game control instance
		gameControl.onHealthChangedCallback += UpdateHealth; // call UpdateHealth() here when the health changes
		this.maxHealth = gameControl.GetMaxHealth();  // grab the max health from game control
		/* health bar and text objects */
		this.bar = GameObject.Find("Health Bar").GetComponent<RectTransform>();
		this.text = GameObject.Find("Health Text").GetComponent<Text>();
		this.textPanel = GameObject.Find("Health Text Background");
		/* health bar geometry */
		this.baseWidth = this.bar.rect.width;
		this.width = this.baseWidth;
	}

	/*
	 * Callback function for change of health.
	 */
	public void UpdateHealth() {
		this.health = gameControl.GetHealth();	// get the new health
		this.text.text = "HP: " + this.health + "/" + this.maxHealth;	// update health text
		float newWidth = this.baseWidth * ((float) this.health / (float) this.maxHealth);   // calculate new width
		StartCoroutine(DecreaseBar(newWidth, 60));  // animate health decrease
	}

	/*
	 * Animation to decrease the health bar.
	 * float newWidth: the new width of the health bar after the decrease
	 * int frames: the number of frames to animate the decrease over
	 */
	private IEnumerator DecreaseBar(float newWidth, int frames) {
		float diff = (this.width - newWidth) / (float) frames; // the amount to decrease the bar by each frame
		float w;
		textPanel.GetComponent<Image>().color = new Color(255, 0, 0);
		for (int i = 1; i < frames + 1; i++) {
			w = this.width - (diff * i);
			this.bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);  // reduce width of bar
			yield return null;
		}
		textPanel.GetComponent<Image>().color = new Color(255, 255, 255);
		this.width = newWidth; // update newWidth once animation is complete
		yield return null;
	}
}
