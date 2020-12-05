using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesSurvivedDisplay : MonoBehaviour {

	public TextMeshProUGUI text;

    void Start() {
		this.text.text = "WAVES SURVIVED: " + StaticInfo.wavesSurvived;
    }
}
