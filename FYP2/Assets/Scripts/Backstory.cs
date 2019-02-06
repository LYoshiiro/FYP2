﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backstory : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;

// Data Values
	[SerializeField] private float fTimer;
	[SerializeField] private int iSlide;
	[SerializeField] private List<Sprite> lSprite;
	private Image iImage;
	private Text tText;
	private string sDialogue;

	private void Start() {
	// Set Game paused
		rCore.bPause = true;
	// Set Reference
		iImage = transform.GetComponent<Image>();
		tText = transform.GetComponentInChildren<Text>();
	// set Initial Value
		fTimer = 0.0f;
		iSlide = 0;
	}

	private void FixedUpdate() {
	// Update Time Limiter
		if (fTimer < 5.0f) {
			fTimer += Time.deltaTime;
		}
		else {
		// Reset the Timer
			fTimer = 0.0f;
		// Update the Slide Count
			iSlide += 1;
		// Exit Condition
			if (iSlide == 5) {
			// Unpause the Game
				rCore.bPause = false;
			// Hide the GameObject
				transform.gameObject.SetActive(false);
				return;
			}
		// Update the sprite slide being showned
			iImage.sprite = lSprite.ToArray()[iSlide];
		// Feed in pre-determined line
			switch (iSlide) {
				case 0: sDialogue = "On the flight to China for a Family Reunion, the plane crossed into a field of dark clouds."; break;
				case 1: sDialogue = "Navigating the blacken sky, the plane was stuck down by lightning."; break;
				case 2: sDialogue = "With the hull punctured and the control system onboard failling, the Plane erupted in flames."; break;
				case 3: sDialogue = "With the sudden plumet, A stray object flying overhead, knocked you unconcious."; break;
				case 4: sDialogue = "Waking up by the rough crashing waves, you spot a distant island. With all your remainding strength, you swam to the island before collapsing."; break;
				default: sDialogue = "Missing Dialogue Here!"; break;
			}
		// Update the text string
			tText.text = sDialogue;
		}
	}
}
