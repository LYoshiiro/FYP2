using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;

// List Reference
	[SerializeField] private Text tFPS;
	[SerializeField] private List<Transform> lTransform;

	private void FixedUpdate() {
    // Check if game is paused
        if (rCore.bPause != true) {
		// FPS
			tFPS.text = "FPS: " + 1 / Time.deltaTime;
		}

	// Update Pop-up
		lTransform.ToArray()[0].gameObject.SetActive(rCore.bWin);
	// Check if game is won
		if (rCore.bWin != true)
			lTransform.ToArray()[1].gameObject.SetActive(rCore.bPause);
	}
}
