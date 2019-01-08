using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
// Global Variables
	[SerializeField] public bool bWin;
	[SerializeField] public bool bPause;

private void FixedUpdate() {
// Check for if pause button was pressed
	if (Input.GetKeyDown(KeyCode.P))
	// Check if the game is already over
		if (bWin != true)
			bPause = !bPause;
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