using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveButtonUI : MonoBehaviour
{
	private Text buttonText;
	private Text frameText;
	private GameObject button;
	public string nextWaveText = "NEXT WAVE";
	private WaveControl waveControl;
	private GameControl gameControl;

	void Start() {
		buttonText = GameObject.Find("WaveButton Text").GetComponent<Text>();
		frameText = GameObject.Find("WaveButton Frame Text").GetComponent<Text>();
		button = GameObject.Find("WaveButton Button");
		waveControl = WaveControl.instance;
		gameControl = GameControl.instance;
		waveControl.onWaveReadyCallback += ButtonCountdown;
		waveControl.onWaveStartedCallback += HideButton;
		waveControl.onWaveFinishedCallback += ShowButton;
	}

	private void ButtonCountdown() {
		StartCoroutine(Countdown());
		button.SetActive(false);
	}

	private void HideButton() {
		//this.gameObject.SetActive(false);
		buttonText.text = nextWaveText;
	}

	private void ShowButton() {
		button.SetActive(true);
		//this.gameObject.SetActive(true);
	}

	private IEnumerator Countdown() {
		for (int t = waveControl.GetCountdown(); t > 0; t--) {
			frameText.text = t.ToString();
			yield return new WaitForSeconds(1);
		}
		frameText.text = "IN PROGRESS";
		yield return null;
	}

}
