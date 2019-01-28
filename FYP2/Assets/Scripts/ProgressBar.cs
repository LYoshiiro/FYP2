using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInteraction rPlayerInteraction;

// Progress Bar
	[SerializeField] private float fProgress;
	[SerializeField] private List<Image> lImages;

// Alignment
	[SerializeField] private Vector3 v3Offset;

	private void Start() {
	// Set all Images to inactive first
		foreach (Image img in lImages)
			img.gameObject.SetActive(false);
	}

	private void Update() {
	// Move placement to follow mouse
		transform.position = Input.mousePosition + v3Offset;
	}

	private void LateUpdate() {
	// Set active only when mouse is held down
		foreach (Image img in lImages)
			img.gameObject.SetActive(rPlayerInteraction.bProgressBar);

	// If not completed loading cycle
		if (fProgress < 1.0f) {
			fProgress = rPlayerInteraction.fBounceTime / (1.5f - rPlayerInteraction.GetModifier());
			lImages.ToArray()[1].fillAmount = fProgress;
		}
		else 
			fProgress = 0.0f;
	}
}
