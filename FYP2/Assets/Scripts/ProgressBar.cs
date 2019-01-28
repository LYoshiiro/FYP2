using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInteraction rPlayerInteraction;
	[SerializeField] private PlayerStatus rPlayerStatus;

// Progress Bar
	[SerializeField] private float fProgress;
	[SerializeField] private List<Image> lImages;
	[SerializeField] private int iType;
	[SerializeField] private Text tText;

// Alignment
	[SerializeField] private Vector3 v3Offset;

	private void Start() {
	// Set all Images to inactive first
		foreach (Image img in lImages)
			img.gameObject.SetActive(false);
	}

	private void Update() {
		if (iType == 0)
		// Move placement to follow mouse
			transform.position = Input.mousePosition + v3Offset;
	}

	private void LateUpdate() {
		switch (iType) {
			case 0: // Interact
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
			break;
			case 1: // Cold
			// Set active
				foreach (Image img in lImages)
					img.gameObject.SetActive(true);

				tText.text = "Cold: " + rPlayerStatus.GetStatus(0) + " / 10";
				fProgress = (float)rPlayerStatus.GetStatus(0) / 10;
				lImages.ToArray()[1].fillAmount = fProgress;
			break;
			case 2: // Energy
			// Set active
				foreach (Image img in lImages)
					img.gameObject.SetActive(true);

				tText.text = "Energy: " + rPlayerStatus.GetStatus(1) + " / 10";
				fProgress = (float)rPlayerStatus.GetStatus(1) / 10;
				lImages.ToArray()[1].fillAmount = fProgress;
			break;
		}
	}
}
