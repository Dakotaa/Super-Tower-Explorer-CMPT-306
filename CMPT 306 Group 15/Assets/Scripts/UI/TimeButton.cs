using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButton : MonoBehaviour {

	private SpeedControl speedcontrol;

    void Start() {
		this.speedcontrol = SpeedControl.instance;
    }

	public void ChangeSpeed(int speed) {
		speedcontrol.SetSpeed(speed);
	}

    
}
