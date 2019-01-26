﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
// References
	[SerializeField] private Core rCore;
	[SerializeField] private Camera rCamera;
	[SerializeField] private List<Button> lButtons;

	private void Start() {
	// Set Initial Values
		rCamera = Camera.main;
	}

// Function for the button to start the game
	public void StartGame() {
		SceneManager.LoadScene("Gameplay");
	}

// Function for the button to quit the game
	public void QuitGame() {
		Application.Quit();
	}

// Function for toggling when Start is hovered
	public void HighlightStart(int toggle) {
		switch (toggle) {
			case 0:
			// Turn off 'No'
				lButtons.ToArray()[0].gameObject.SetActive(false);
			// Turn on 'Yes'
				lButtons.ToArray()[1].gameObject.SetActive(true); break;
			case 1:
			// Turn off 'Yes'
				lButtons.ToArray()[1].gameObject.SetActive(false);
			// Turn on 'No'
				lButtons.ToArray()[0].gameObject.SetActive(true); break;
			default: rCore.Pnt("Missing Info: Start Button"); break;
		}
	}

// Function for toggling when Quit is hovered
	public void HighlightQuit(int toggle) {
		switch (toggle) {
			case 0:
			// Turn off 'No'
				lButtons.ToArray()[2].gameObject.SetActive(false);
			// Turn on 'Yes'
				lButtons.ToArray()[3].gameObject.SetActive(true); break;
			case 1:
			// Turn off 'Yes'
				lButtons.ToArray()[3].gameObject.SetActive(false);
			// Turn on 'No'
				lButtons.ToArray()[2].gameObject.SetActive(true); break;
			default: rCore.Pnt("Missing Info: Quit Button"); break;
		}
	}
}
