using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour{

	/* instance variables */
	public static ToolTipController instance;
	private GameObject tooltip;
	private RectTransform tooltipBackground;
	private Text tooltipText;
	private string text;
	private bool visible = false;
	TextGenerator textGen = new TextGenerator();

	#region Singleton

	void Awake() {
		/* singleton initialization */
		if (instance != null) {
			Debug.LogWarning("More than one instance of Tooltip Controller found!");
			return;
		}
		instance = this;

		this.tooltip = GameObject.Find("Tooltip");
		this.tooltipBackground = GameObject.Find("Tooltip Background").GetComponent<RectTransform>();
		this.tooltipText = GameObject.Find("Tooltip Text").GetComponent<Text>();
		tooltipText.supportRichText = true;
		this.Hide();
	}

	#endregion

	public void Update() {
		if (this.visible) {
			this.Relocate();
		}
	}

	/* sets the current tooltip text */
	public void Set(string text) {
		this.text = text;
		this.tooltipText.text = text;
		// resize
		TextGenerationSettings generationSettings = tooltipText.GetGenerationSettings(tooltipText.rectTransform.rect.size);
		float width = textGen.GetPreferredWidth(text, generationSettings);
		float height = textGen.GetPreferredHeight(text, generationSettings);
		this.tooltipBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		this.tooltipBackground.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		this.tooltipBackground.ForceUpdateRectTransforms();
		Canvas.ForceUpdateCanvases();
	}

	/* makes the tooltip visible with the current text */
	public void Show() {
		this.tooltip.SetActive(true);
		this.visible = true;
	}

	/* sets the current tooltip text and makes it visible */
	public void SetAndShow(string text) {
		this.Set(text);
		this.Show();
	}

	/* hides the tooltip */
	public void Hide() {
		this.tooltip.SetActive(false);
		this.visible = false;
	}

	/* moves the tooltip to the cursor */
	public void Relocate() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.y -= 10;
		this.tooltip.transform.position = mousePos;
	}

	public bool GetVisible() {
		return this.visible;
	}

	public void Refresh() {
		if (this.visible) {
			this.Hide();
			this.Show();
		}
	}
}
