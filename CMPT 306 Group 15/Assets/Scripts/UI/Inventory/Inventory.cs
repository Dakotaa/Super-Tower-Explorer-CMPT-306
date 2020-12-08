using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}
		instance = this;

		resources.Add("Wood", 0);
		resources.Add("Stone", 0);
		resources.Add("Iron", 0);
	}

	#endregion

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	// Dictionary of resources
	Hashtable resources = new Hashtable();

	public void IncreaseResource(string resource, int amt) {
		if (resources.ContainsKey(resource)) {
			resources[resource] = ((int) resources[resource]) + amt;
		}
		// Trigger callback
		if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
	}

	public void DecreaseResource(string resource, int amt) {
		if (resources.ContainsKey(resource)) {
			resources[resource] = ((int)resources[resource]) - amt;
		}
		// Trigger callback
		if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
	}

	public int GetResourceCount(string resource) {
		if (resources.ContainsKey(resource)) {
			return (int)resources[resource];
		}
		return 0;
	}
}