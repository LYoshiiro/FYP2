using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;

// Interaction Data
	[SerializeField] public bool bObstructed;

	private void Start() {
		bObstructed = false;
	}

// Set the Core reference and set object to obstructed / Function called when environment is spawned on it
	public void SetTile(Core core) {
		rCore = core;
		bObstructed = true;
	}
}
