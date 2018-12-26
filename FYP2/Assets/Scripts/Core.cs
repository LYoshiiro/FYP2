using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
// Global Variables
	[SerializeField] public bool bWin;
	[SerializeField] public bool bPause;

// Print info
	public void Pnt<T>(T V) {
		Debug.Log(V);
	}

}

// Returns Application's Path
// refCore.Print(System.IO.Directory.GetCurrentDirectory());
// Prints the Desktop's Path
// refCore.Print(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));