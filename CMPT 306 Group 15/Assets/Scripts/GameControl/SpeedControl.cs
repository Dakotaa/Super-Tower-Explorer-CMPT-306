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
	}

	#endregion

	int speedModifier = 1;
	public GameObject Button1, Button2, Button3;

	private void Update() {
		if (Input.GetKeyDown("1")) {
			this.SetSpeed(1);
		}
		if (Input.GetKeyDown("2")) {
			this.SetSpeed(2);
		}
		if (Input.GetKeyDown("3")) {
			this.SetSpeed(3);
		}
	}

	public void SetSpeed(int speed) {
		this.speedModifier = speed;
		Time.timeScale = speedModifier;
		switch (speedModifier) {
			case 1:
				Button2.GetComponent<Toggle>().isOn = false;
				Button3.GetComponent<Toggle>().isOn = false;
				break;
			case 2:
				Button1.GetComponent<Toggle>().isOn = false;
				Button3.GetComponent<Toggle>().isOn = false;
				break;
			case 3:
				Button1.GetComponent<Toggle>().isOn = false;
				Button2.GetComponent<Toggle>().isOn = false;
				break;
			default:
				break;
		}
	}
}
