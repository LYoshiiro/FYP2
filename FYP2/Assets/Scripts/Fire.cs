using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private Tile rTile;

	private void FixedUpdate() {
	// Check if the fire is still alive
		if (rTile.bFire != true) {
		// Kill the fire
			transform.gameObject.SetActive(false);
		// Destroy Gameobject
			Destroy(transform.gameObject);
		}
	}

// Set Fire reference
	public void SetFire(Core core, Tile tile) {
		rCore = core;
		rTile = tile;
	}

// Get Tile Reference
	public Tile GetTile() {
		return rTile;
	}
}
