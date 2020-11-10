using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveButton : MonoBehaviour {
	private WaveControl waveControl;
	bool initialized = false;
    private void Initialize() {
		initialized = true;
    }

	public void NextWave() {
		// call Inialize, since start function doesnt work
		waveControl = WaveControl.instance;
		waveControl.NextWave();
	}
}
