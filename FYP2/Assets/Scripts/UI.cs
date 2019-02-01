﻿using System.Collections;
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

	public void ColdDeath() {
	// Set message active
		lTransform.ToArray()[2].gameObject.SetActive(true);
	// Set game to dead
		rCore.bDeath = true;
	}

	public void StarveDeath() {
	// Set message active
		lTransform.ToArray()[3].gameObject.SetActive(true);
	// Set game to dead
		rCore.bDeath = true;
	}

	public void SetIndicator(string type, int call) {
		switch (call) {
		// Turn off the Indicator
			default: lTransform.ToArray()[4].gameObject.SetActive(false); break;
		// Turn on the Indicator(Blank)
			case 1: 
			// Set message text
				lTransform.ToArray()[4].GetComponentInChildren<Text>().text = type;

			// Set message active
				lTransform.ToArray()[4].gameObject.SetActive(true);
			break;
		// Turn on the Indicator(Placing)
			case 2: 
			// Set message text
				lTransform.ToArray()[4].GetComponentInChildren<Text>().text = "Placing: " + type;

			// Set message active
				lTransform.ToArray()[4].gameObject.SetActive(true);
			break;
		// Turn on the Indicator(Crafting)
			case 3:
			// Set message text
				lTransform.ToArray()[4].GetComponentInChildren<Text>().text = "Crafted: " + type;

			// Set message active
				lTransform.ToArray()[4].gameObject.SetActive(true);
			break;
		}
	}
}
