using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
// Class References
	[SerializeField] private Core rCore;
	[SerializeField] private MapValue rValue;

// Gameobject Holder
	[SerializeField] private Transform tMapHolder;

// Map Size
	[SerializeField] private Vector2 v2Size;
	[SerializeField] [Range(0, 1)] private float fDownscale;
	
// Map Value Offsets
	private int iTree;
	private int iStone;
	private int iBush;
	private int iTile;

	private void Start() {
	// Set Initial Values
		iTree = 0;
		iStone = 0;
		iBush = 0;
		iTile = 0;

	// Assign caller string for Map Holder
		string sHolder = "Map Holder";
	// Search for Map Holder : if true; delete it
		if (transform.Find(sHolder))
			DestroyImmediate(transform.Find(sHolder).gameObject);
	// Initialize new Gameobject to Map Holder
		tMapHolder = new GameObject (sHolder).transform;
	// Assign Gameobject parent
		tMapHolder.parent = transform;

	// Do Tile Generation here!
		for (int x = 0; x < v2Size.x; x++) {
			for (int z = 0; z < v2Size.y; z++) {
			// Get Tile Position
				Vector3 v3Pos = new Vector3(-v2Size.x / 2 + 0.5f + x, 0, -v2Size.y / 2 + 0.5f + z);
			// Initialize Tile
				Transform tTile = Instantiate(rValue.ParseList().ToArray()[0], v3Pos, Quaternion.identity) as Transform;
			// Set Tile Parent
				tTile.parent = tMapHolder;
			// Rename Tile
				tTile.name = "T" + iTile++;
			// Downscale Tile Size
				tTile.localScale = Vector3.one * (1 - fDownscale);
			}
		}
	}

}
