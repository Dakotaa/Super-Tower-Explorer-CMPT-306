using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSlot : MonoBehaviour {
	public string resource;
	public int count = 0;

	public string GetResource() {
		return this.resource;
	}

	public void changeCount(int newCount) {
		count = newCount;
		gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "x" + count;
	}
}
