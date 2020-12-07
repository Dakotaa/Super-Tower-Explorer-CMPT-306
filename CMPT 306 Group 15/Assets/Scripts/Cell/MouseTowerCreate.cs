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

	public void Start() {
		this.tooltip = ToolTipController.instance;
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
			resourceCosts += "\n - Metal x" + this.metalCost;
		}
		if (this.woodCost > 0) {
			resourceCosts += "\n - Wood x" + this.woodCost;
		}
		if (this.stoneCost > 0) {
			resourceCosts += "\n - Stone x" + this.stoneCost;
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