using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInteraction rPlayer;

// Progress Bar
	[SerializeField] private float fProgress;
	[SerializeField] private List<Image> lImages;

// Alignment
	private Vector2 v2Position;

	private void Start() {
	// Parse UI placement
	v2Position = transform.position;

	// Set all Images to inactive first
		foreach (Image img in lImages)
			img.gameObject.SetActive(false);
	}

	private void LateUpdate() {
	// Move placement to follow mouse
		transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)+ v2Position;

	// Set active only when mouse is held down
		foreach (Image img in lImages)
			img.gameObject.SetActive(rPlayer.bProgressBar);

	// If not completed loading cycle
		if (fProgress < 1.0f) {
			fProgress = rPlayer.fBounceTime / 1.5f;
			lImages.ToArray()[1].fillAmount = fProgress;
		}
		else 
			fProgress = 0.0f;
	}
}
