using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour {

	#region Singleton

	public static SpeedControl instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of SpeedControl found!");
			return;
		}
		instance = this;
		this.sound = SoundManager.Instance;
	}

	#endregion
	private SoundManager sound;
	private int speedModifier = 1;
	public AudioClip speed1;
	public AudioClip speed2;
	public AudioClip speed3;

	private void Update() {
		if (Input.GetKeyDown("1")) {
			this.speedModifier = 1;
			Time.timeScale = speedModifier;
			sound.Play(speed1, this.transform, 1.0f, 1.0f);
		}
		if (Input.GetKeyDown("2")) {
			this.speedModifier = 2;
			Time.timeScale = speedModifier;
			sound.Play(speed2, this.transform, 1.0f, 1.2f);
		}
		if (Input.GetKeyDown("3")) {
			this.speedModifier = 3;
			Time.timeScale = speedModifier;
			sound.Play(speed3, this.transform, 1.0f, 1.5f);
		}
	}

	public int GetSpeed() {
		return this.speedModifier;
	}

	public void SetSpeed(int speed) {
		print("Set speed to " + speed);
		this.speedModifier = speed;
		Time.timeScale = speedModifier;
	}
}