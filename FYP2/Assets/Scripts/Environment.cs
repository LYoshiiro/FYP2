using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
// Class Reference
	[SerializeField] public Core rCore;
	[SerializeField] private Tile rTile;

// Data Values
	[SerializeField] public string sNode;

// Set data for references
	public void SetData(Core core, Tile tile, string node) {
		rCore = core;
		rTile = tile;
		sNode = node;
	}

	public void Despawn() {
	// Hide Updating GameObject
		transform.GetComponentInChildren<MeshRenderer>().enabled = false;
	// Reset the Tile to not obstructed
		rTile.isObstructed = false;
	// Destroy old GameObject
		Destroy(transform.gameObject);
	}
}
