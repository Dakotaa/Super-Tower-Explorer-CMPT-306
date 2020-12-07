using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class used to move the tower picture in build menu around on screen by clicking
public class MouseTowerCreate : MonoBehaviour, IPointerClickHandler
{
    public bool isTowerDragged = false;
	public string name;
    public int woodCost;
    public int stoneCost;
    public int metalCost;
    private Vector3 resetPosition = new Vector3(27.5f, -27.5f);
	private ToolTipController tooltip;
	private Inventory inventory;

	public void Start() {
		this.tooltip = ToolTipController.instance;
		this.inventory = Inventory.instance;
	}

	// Moves the tower picture when mouse button clicked
	public void OnPointerClick(PointerEventData eventData)
    {
        if (!isTowerDragged)
        {
            isTowerDragged = true;
        }
        else
        {
            isTowerDragged = false;
        }
    }

	public void ShowTooltip() {
		string resourceCosts = "";
		if (this.metalCost > 0) {
			if (this.inventory.GetResourceCount("Iron") >= this.metalCost) {
				resourceCosts += "\n<color=green> - Metal x" + this.metalCost + "</color>";
			} else {
				resourceCosts += "\n -<color=maroon> Metal x" + this.metalCost + "</color>";

			}
		}
		if (this.stoneCost > 0) {
			if (this.inventory.GetResourceCount("Stone") >= this.stoneCost) {
				resourceCosts += "\n -<color=green> Stone x" + this.stoneCost + "</color>";
			} else {
				resourceCosts += "\n -<color=maroon> Stone x" + this.stoneCost + "</color>";

			}
		}
		if (this.woodCost > 0) {
			if (this.inventory.GetResourceCount("Wood") >= this.woodCost) {
				resourceCosts += "\n<color=green> - Wood x" + this.woodCost + "</color>";
			} else {
				resourceCosts += "\n -<color=maroon> Wood x" + this.woodCost + "</color>";

			}
		}
		tooltip.SetAndShow(this.name + "\nCost: " + resourceCosts);
	}

	public void HideTooltip() {
		tooltip.Hide();
	}

    // Moves the tower picture when mouse button clicked
    public void Update()
    {
        if (isTowerDragged)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            transform.localPosition = resetPosition;
        }
    }
}