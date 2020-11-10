using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles the visual component of the player's EXO
 * This includes the EXP bar and text
 * Callback functions depend on an instane of GameControl.
 * Mostly analogous to HealthBarUI
 */

public class EXPBarUI : MonoBehaviour {
	private float EXPToLevel;  // EXP needed to level up (max EXP)
	private float EXP; // current health
	private float baseWidth; // starting width of the EXP bar image
	private float width; // current width
	private RectTransform bar;	// green bar image
	private Text text;  // HP display text
	private GameObject textPanel;
	private GameControl gameControl; // Game Control instance

	void Start() {
		/* game control setup */
		gameControl = GameControl.instance; // grab the game control instance
		gameControl.OnEXPChangedCallback += UpdateEXP; // call UpdateEXP() here when the EXP changes
		this.EXP = gameControl.GetEXP();
		this.EXPToLevel = gameControl.GetNextLevelEXP();  // grab the max health from game control
		/* EXP bar and text objects */
		this.bar = GameObject.Find("EXP Bar").GetComponent<RectTransform>();
		this.text = GameObject.Find("EXP Text").GetComponent<Text>();
		this.textPanel = GameObject.Find("EXP Text Background");
		/* EXP bar geometry */
		this.baseWidth = this.bar.rect.width;
		this.width = 0;
		this.text.text = "EXP: " + this.EXP + "/" + this.EXPToLevel;    // update EXP text
		this.bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
	}

	/*
	 * Callback function for change of EXP
	 */
	public void UpdateEXP() {
		this.EXP = gameControl.GetEXP();	// get the new EXP
		this.text.text = "EXP: " + this.EXP + "/" + this.EXPToLevel;	// update EXP text
		float newWidth = this.baseWidth * ( this.EXP / this.EXPToLevel);   // calculate new width
		StartCoroutine(ChangeBar(newWidth, 60));  // animate health decrease
	}

	/*
	 * Animation to decrease the health bar.
	 * float newWidth: the new width of the health bar after the decrease
	 * int frames: the number of frames to animate the decrease over
	 */
	private IEnumerator ChangeBar(float newWidth, int frames) {
		float diff = (this.width - newWidth) / (float) frames; // the amount to decrease the bar by each frame
		float w;
		textPanel.GetComponent<Image>().color = new Color(0, 255, 0);
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
