using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
// Class Reference
	[SerializeField] public Core rCore;
	[SerializeField] private Tile rTile;

// Data Values
	[SerializeField] public string sNode;
	[SerializeField] private float fFireDegrade;

	private void FixedUpdate() {
	// Check Fire Status
		if (rTile.bFire == true) {
		// Check Fire lifespan
			if (fFireDegrade < 10.5f)
			// Propergade Fire lifespan
				fFireDegrade += Time.deltaTime;
			else
			// Destroy Environment Object
				Despawn();
		}
	// If Fire was put out
		else
		// Reset Degrade Timer
			fFireDegrade = 0.0f;
	}

// Set data for references
	public void SetData(Core core, Tile tile, string node) {
		rCore = core;
		rTile = tile;
		sNode = node;
		fFireDegrade = 0;
	}

// Despawn Function
	public void Despawn() {
	// Hide Updating GameObject
		transform.GetComponentInChildren<MeshRenderer>().enabled = false;
	// Reset the Tile to not obstructed
		rTile.bObstructed = false;
	// Reset Fire Status
		fFireDegrade = 0;
		rTile.bFire = false;
	// Destroy old GameObject
		Destroy(transform.gameObject);
	}

// Get the Tile Reference
	public Transform GetTile() {
		return rTile.transform;
	}
}
