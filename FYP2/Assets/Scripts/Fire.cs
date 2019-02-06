using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private Tile rTile;
	private MapGenerator rMapGenerator;
	private Transform tMapHolder;
	private Vector2 v2Size;

// Modifier
	[SerializeField] private float fTime;
	private bool bSpread;

	private void Start() {
	// Set Initial Value
		fTime = 0.0f;
	}

	private void FixedUpdate() {
	// Check if the fire is still alive
		if (rTile.bFire != true) {
		// Kill the fire
			transform.gameObject.SetActive(false);
		// Destroy Gameobject
			Destroy(transform.gameObject);
		}
	// Spread Wild Fire
		else {
		// Check if fire is allowed to spread
			if (bSpread == true) {
			// Get Tile Number
				int iTile = rTile.GetNumber();
			// Check if Map Holder is valid
				if (tMapHolder != null) {
				// Create Transform List for Wild Fire
					List<Transform> lSpread = new List<Transform>();
				// Set Transform for Spread Tiles
					if (iTile > 0) {
						lSpread.Add(tMapHolder.GetChild(iTile - (int)v2Size.x));
						lSpread.Add(tMapHolder.GetChild(iTile - 1));
					}
					if (iTile < (v2Size.x * v2Size.y - 1)) {
						lSpread.Add(tMapHolder.GetChild(iTile + 1));
						lSpread.Add(tMapHolder.GetChild(iTile + (int)v2Size.x));
					}
				// Loop through the 4 axis adjacent tiles
					for (int i = 0; i < lSpread.ToArray().Length; i++) {
					// Roll for Wild Fire
						if (Random.Range(0, 20) == 4) {
						// Check for Issues
							if ((lSpread.ToArray()[i].GetComponent<Tile>().bObstructed != true) && (lSpread.ToArray()[i].GetComponent<Tile>().bFire != true)) {
								rMapGenerator.SpawnFire(lSpread.ToArray()[i], false);
							}
						}
					}
				}
			}
		// If not allowed to Spread, let the fire decay
			else {
				if (fTime < 10.5f)
					fTime += Time.deltaTime;
				else {
					rTile.bFire = false;
				}
			}
		}
	}

// Set Fire reference
	public void SetFire(Core core, Tile tile, MapGenerator mapGenerator, Transform mapHolder, Vector2 size, bool spread) {
		rCore = core;
		rTile = tile;
		rMapGenerator = mapGenerator;
		tMapHolder = mapHolder;
		v2Size = size;
		bSpread = spread;
	}

// Get Tile Reference
	public Tile GetTile() {
		return rTile;
	}
}
