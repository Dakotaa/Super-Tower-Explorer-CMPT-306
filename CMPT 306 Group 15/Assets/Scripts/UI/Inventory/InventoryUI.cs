using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public List<ResourceSlot> resourceSlots;
	Inventory inventory;
	void Start() {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
	}

	void UpdateUI() {
		// update each slot by getting the resource of that slot, then checking the
		// resource count of that resource in the inventory manager
		foreach (ResourceSlot slot in resourceSlots) {
			slot.changeCount(inventory.GetResourceCount(slot.GetResource()));
		}
	}
}
