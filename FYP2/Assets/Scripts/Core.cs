﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
// Reference
	[SerializeField] private PlayerStatus rPlayerStatus; 
	[SerializeField] private Transform tLight;
	[SerializeField] private Rain rRain;
	[SerializeField] private Tornado rTornado;

// Global Variables
	[SerializeField] public bool bWin;
	[SerializeField] public bool bPause;
	[SerializeField] public bool bDeath;
	[SerializeField] public bool bRain;
	[SerializeField] public bool bTornado;
	[SerializeField] public float fTime;
	[SerializeField] public int iDays;

	private void Start() {
	// Set the timescale to match real time
		Time.timeScale = 1.0f;
	// Set Initial Values
		fTime = 0.0f;
		iDays = 0;
		bRain = false;
		bTornado = false;
	}

	private void FixedUpdate() {
	// Check for if pause button was pressed
		if (Input.GetKeyDown(KeyCode.P) && bDeath != true)
		// Check if the game is already over
			if (bWin != true) {
				bPause = !bPause;
			}

	// Check if Game is running
		if (bPause != true) {
		// Move Time as Game Progresses
			fTime += Time.deltaTime;
		// 1 Full Cycle (5mins)
			if (fTime > 150.0f) {
			// Update Day
				iDays += 1;
			// Reset Time
				fTime = 0.0f;
			// Increase Coldness
				rPlayerStatus.Freezing();
			// Increase Energy
				rPlayerStatus.Rest(5);
			// Generate Chance of Rain
				bRain = (UnityEngine.Random.Range(0, 2) == 1);
			// Generate Chance of Tornado
				bTornado = (UnityEngine.Random.Range(0, 13) == 12);
			}
			
		// Update Skybox
			Quaternion qRotation = Quaternion.Euler((fTime / 150.0f * 360), 135, 0);
			tLight.rotation = qRotation;
			// tLight.RotateAround(tLight.position, Vector3.right, ((fTime / 150.0f) * 360.0f));
			// Pnt(fTime / 150.0f * 360);

		// Update Rain
			rRain.gameObject.SetActive(bRain);
		// Update Tornado
			rTornado.gameObject.SetActive(bTornado);
		}

	// Pause the Whole Game Cause Player Died
		if (bDeath == true)
			bPause = true;
	}

// Print info
	public void Pnt<T>(T V) {
		Debug.Log(V);
	}

// Get the size of collider
	public Vector3 BoundLengths(Transform T) {
		Collider collider = T.GetComponent<Collider>();
		return collider.bounds.size;
	}

// CSV Reader
	public string[] CSVRead(string file) {
	// Catch
        if (!File.Exists(file)) {
            File.Create(file);
			Pnt("File was Created!");
		}

	// Get Status of the File that is being checked
		// Pnt(File.GetAttributes(file));

	// Get all the lines from the text file
		string[] sAry = File.ReadAllLines(file);

		return sAry;
	}
}
// Returns Application's Path
// refCore.Print(System.IO.Directory.GetCurrentDirectory());
// Prints the Desktop's Path
// refCore.Print(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));