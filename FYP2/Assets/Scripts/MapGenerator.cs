using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
// Class References
	[SerializeField] private Core rCore;
	[SerializeField] private MapValue rValue;
	[SerializeField] private PlayerStatus rPlayerStatus;

// Gameobject Holder
	[SerializeField] private Transform tMapHolder;
	[SerializeField] private Transform tEnvironmentHolder;
	[SerializeField] private Transform tPlacementHolder;

// Map Size
	[SerializeField] private Vector2 v2Size;
	[SerializeField] [Range(0, 1)] private float fDownscale;
	
// Map Value Origins
	private int[] iOrigins = new int[4]; // Has to be declared here due to L83
	private int iHalf;

// Map Value Limiters
	[SerializeField] private int iTree;
	[SerializeField] private int iStone;
	[SerializeField] private int iBerry;
	[SerializeField] private int iCotton;
	[SerializeField] private int[] iTent;
	private int iTile;
	private List<int> lSpawnOffset;
	private bool bInitial;

// Respawner
	[SerializeField] private int iNext;

	private void Start() {
	// Set Initial Variables
		iTree = 0;
		iStone = 0;
		iBerry = 0;
		iCotton = 0;
		iTile = 0;
		lSpawnOffset = new List<int>();
		bInitial = false;
		
		iNext = Random.Range(2, 5);

	// Assign caller string for Holder Object(s)
		string sMapHolder = "Map Holder";
		string sEnvironmentHolder = "Environment Holder";
		string sPlacementHolder = "Placement Holder";
	// Search for Holder Object(s) : if true; delete it
		if (transform.Find(sMapHolder))
			DestroyImmediate(transform.Find(sMapHolder).gameObject);
		if (transform.Find(sEnvironmentHolder))
			DestroyImmediate(transform.Find(sEnvironmentHolder).gameObject);
	// Initialize new Gameobject to Holder Object(s)
		tMapHolder = new GameObject (sMapHolder).transform;
		tEnvironmentHolder = new GameObject (sEnvironmentHolder).transform;
		tPlacementHolder = new GameObject (sPlacementHolder).transform;
	// Assign Gameobject parent
		tMapHolder.parent = transform;
		tEnvironmentHolder.parent = transform;
		tPlacementHolder.parent = transform;

	// Do Tile Generation here!
		for (int x = 0; x < v2Size.x; x++) {
			for (int z = 0; z < v2Size.y; z++) {
			// Get Tile Position
				Vector3 v3Pos = new Vector3(-v2Size.x / 2 + 0.5f + x, 0, -v2Size.y / 2 + 0.5f + z);
			// Initialize Tile
				Transform tTile = Instantiate(rValue.lPrefabs.ToArray()[0], v3Pos, Quaternion.identity) as Transform;
			// Set Tile Parent
				tTile.parent = tMapHolder;
			// Rename Tile
				tTile.name = "T" + iTile++;
			// Assign Number
				tTile.GetComponentInChildren<Tile>().SetNumber(iTile - 1);
			// Downscale Tile Size
				tTile.localScale = Vector3.one * (1 - fDownscale);
			}
		}

	// Set iHalf
		iHalf = Mathf.RoundToInt(v2Size.x / 2);
	// Store Origins
		iOrigins[0] = Mathf.RoundToInt((iHalf - 1) * v2Size.x + iHalf - 1);
		iOrigins[1] = Mathf.RoundToInt((iHalf - 1) * v2Size.x + iHalf);
		iOrigins[2] = Mathf.RoundToInt((v2Size.x - (iHalf - 1)) * v2Size.x - iHalf);
		iOrigins[3] = Mathf.RoundToInt((v2Size.x - (iHalf - 1)) * v2Size.x - (iHalf + 1));
	// Print Origin points
		// rCore.Pnt(iOrigins[0] + ", " + iOrigins[1] + ", " + iOrigins[2] + ", " + iOrigins[3]);
	}

	private void FixedUpdate() {
	// Generate Environment Objects
		if (bInitial == false) {
			GenerateEnvironment();
			bInitial = true;
		}
		else {
		// Check if the Core reference object exists
			if (rCore != null) {
			// Generate Next time the respawner will kick in
				if (iNext <= rCore.iDays) {
					iNext = Random.Range(2, 5) + rCore.iDays;
					Spawner();
				}
			}
		}

	// Firespawning
		if (tPlacementHolder.childCount != 0) {
		// Get Fireplace
			Transform tFireplace = tPlacementHolder.Find("Fireplace");
		// Create list for trees
			List<Transform> lTrees = new List<Transform>();
		// Get Trees
			foreach (Transform child in tEnvironmentHolder) {
			// Search via Environment's sNode Value
				if (child.GetComponent<Environment>().sNode == "Wood") {
					lTrees.Add(child);
				}
			}

		// Create distance array
			float[] fTreeDistance = new float[lTrees.ToArray().Length];
		// Get the distance
			for (int i = 0; i < lTrees.ToArray().Length; i++) {
				fTreeDistance[i] = Vector3.Magnitude(tFireplace.position - lTrees[i].position);
			}

		// Spawn Fire
			for (int j = 0; j < lTrees.ToArray().Length; j++) {
			// Check distance
				if (fTreeDistance[j] < 2) {
					SpawnFire(lTrees[j].GetComponent<Environment>().GetTile());
				}
			}
		}
	}

// Generate Environment Objects
	private void GenerateEnvironment() {
	// Check the holder for avaliability
		if (tMapHolder != null) {
		// Check if limit is reached
		// Tree =========================================================
			while (iTree < rValue.iTree) {
			// Update limiter
				iTree++;
			// random generate position of Environment
				int iChild = Random.Range(1, tMapHolder.childCount);
			// Check for overstepping
				if (lSpawnOffset == null) while (iOrigins.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// In case its spawning on a repeated tile
				else while (iOrigins.Contains(iChild) == true || lSpawnOffset.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// Update Spawn Offset
				lSpawnOffset.Add(iChild);
			// Get Tile of randomized location
				Transform tPlacement = tMapHolder.GetChild(iChild).transform;
			// Instantiate new Tree
				Transform tTree = Instantiate(rValue.lPrefabs.ToArray()[1], tPlacement.position, Quaternion.identity) as Transform;
			// Assign Parent Object
				tTree.parent = tEnvironmentHolder;
			// Assign Name
				tTree.name = "Tree " + iTree;
			// Parse Reference Core
				tTree.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Wood");
			// Update Tile
				tPlacement.GetComponent<Tile>().SetTile(rCore);
			}
		// Stone =========================================================
			while (iStone < rValue.iStone) {
			// Update limiter
				iStone++;
			// random generate position of Environment
				int iChild = Random.Range(1, tMapHolder.childCount);
			// Check for overstepping
				if (lSpawnOffset == null) while (iOrigins.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// In case its spawning on a repeated tile
				else while (iOrigins.Contains(iChild) == true || lSpawnOffset.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// Update Spawn Offset
				lSpawnOffset.Add(iChild);
			// Get Tile of randomized location
				Transform tPlacement = tMapHolder.GetChild(iChild).transform;
			// Instantiate new Stone
				Transform tStone = Instantiate(rValue.lPrefabs.ToArray()[2], tPlacement.position, Quaternion.identity) as Transform;
			// Assign Parent Object
				tStone.parent = tEnvironmentHolder;
			// Assign Name
				tStone.name = "Stone " + iStone;
			// Parse Reference Core
				tStone.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Stone");
			// Update Tile
				tPlacement.GetComponent<Tile>().SetTile(rCore);
			}
		// Berry =========================================================
			while (iBerry < rValue.iBerry) {
			// Update limiter
				iBerry++;
			// random generate position of Environment
				int iChild = Random.Range(1, tMapHolder.childCount);
			// Check for overstepping
				if (lSpawnOffset == null) while (iOrigins.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// In case its spawning on a repeated tile
				else while (iOrigins.Contains(iChild) == true || lSpawnOffset.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// Update Spawn Offset
				lSpawnOffset.Add(iChild);
			// Get Tile of randomized location
				Transform tPlacement = tMapHolder.GetChild(iChild).transform;
			// Instantiate new Berry
				Transform tBerry = Instantiate(rValue.lPrefabs.ToArray()[3], tPlacement.position, Quaternion.identity) as Transform;
			// Assign Parent Object
				tBerry.parent = tEnvironmentHolder;
			// Assign Name
				tBerry.name = "Berry " + iBerry;
			// Parse Reference Core
				tBerry.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Berry");
			// Update Tile
				tPlacement.GetComponent<Tile>().SetTile(rCore);
			}
		// Berry =========================================================
			while (iCotton < rValue.iCotton) {
			// Update limiter
				iCotton++;
			// random generate position of Environment
				int iChild = Random.Range(1, tMapHolder.childCount);
			// Check for overstepping
				if (lSpawnOffset == null) while (iOrigins.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// In case its spawning on a repeated tile
				else while (iOrigins.Contains(iChild) == true || lSpawnOffset.Contains(iChild) == true)
						iChild = Random.Range(1, tMapHolder.childCount);
			// Update Spawn Offset
				lSpawnOffset.Add(iChild);
			// Get Tile of randomized location
				Transform tPlacement = tMapHolder.GetChild(iChild).transform;
			// Instantiate new Cotton
				Transform tCotton = Instantiate(rValue.lPrefabs.ToArray()[4], tPlacement.position, Quaternion.identity) as Transform;
			// Assign Parent Object
				tCotton.parent = tEnvironmentHolder;
			// Assign Name
				tCotton.name = "Cotton " + iCotton;
			// Parse Reference Core
				tCotton.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Cotton");
			// Update Tile
				tPlacement.GetComponent<Tile>().SetTile(rCore);
			}
		}
	}

// Spawn Environment Objects
	public void Spawner() {
		// Check the holder for avaliability
		if (tMapHolder != null) {
		// RNG Roll for spawn
			if (Random.Range(0, 11) > 6) {
		// Check if limit is reached
			// Tree =========================================================
				if (iTree < rValue.iTree) {
				// Update limiter
					iTree++;
				// random generate position of Environment
					int iChild = Random.Range(1, tMapHolder.childCount);
				// Loop so that the origin doesn't get spawned on and that the tile that it is being placed on doesnt isnt already obstructed
					while (iOrigins.Contains(iChild) == true || tMapHolder.GetChild(iChild).GetComponent<Tile>().bObstructed == true)
						iChild = Random.Range(1, tMapHolder.childCount);
				// Get Tile of randomized location
					Transform tPlacement = tMapHolder.GetChild(iChild).transform;
				// Instantiate new Tree
					Transform tTree = Instantiate(rValue.lPrefabs.ToArray()[1], tPlacement.position, Quaternion.identity) as Transform;
				// Assign Parent Object
					tTree.parent = tEnvironmentHolder;
				// Parse Reference Core
					tTree.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Wood");
				// Update Tile
					tPlacement.GetComponent<Tile>().SetTile(rCore);
				}
			// Stone =========================================================
				else if (iStone < rValue.iStone) {
				// Update limiter
					iStone++;
				// random generate position of Environment
					int iChild = Random.Range(1, tMapHolder.childCount);
				// Loop so that the origin doesn't get spawned on and that the tile that it is being placed on doesnt isnt already obstructed
					while (iOrigins.Contains(iChild) == true || tMapHolder.GetChild(iChild).GetComponent<Tile>().bObstructed == true)
						iChild = Random.Range(1, tMapHolder.childCount);
				// Update Spawn Offset
					lSpawnOffset.Add(iChild);
				// Get Tile of randomized location
					Transform tPlacement = tMapHolder.GetChild(iChild).transform;
				// Instantiate new Stone
					Transform tStone = Instantiate(rValue.lPrefabs.ToArray()[2], tPlacement.position, Quaternion.identity) as Transform;
				// Assign Parent Object
					tStone.parent = tEnvironmentHolder;
				// Parse Reference Core
					tStone.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Stone");
				// Update Tile
					tPlacement.GetComponent<Tile>().SetTile(rCore);
				}
			// Berry =========================================================
				else if (iBerry < rValue.iBerry) {
				// Update limiter
					iBerry++;
				// random generate position of Environment
					int iChild = Random.Range(1, tMapHolder.childCount);
				// Loop so that the origin doesn't get spawned on and that the tile that it is being placed on doesnt isnt already obstructed
					while (iOrigins.Contains(iChild) == true || tMapHolder.GetChild(iChild).GetComponent<Tile>().bObstructed == true)
						iChild = Random.Range(1, tMapHolder.childCount);
				// Update Spawn Offset
					lSpawnOffset.Add(iChild);
				// Get Tile of randomized location
					Transform tPlacement = tMapHolder.GetChild(iChild).transform;
				// Instantiate new Berry
					Transform tBerry = Instantiate(rValue.lPrefabs.ToArray()[3], tPlacement.position, Quaternion.identity) as Transform;
				// Assign Parent Object
					tBerry.parent = tEnvironmentHolder;
				// Parse Reference Core
					tBerry.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Berry");
				// Update Tile
					tPlacement.GetComponent<Tile>().SetTile(rCore);
				}
			// Berry =========================================================
				else if (iCotton < rValue.iCotton) {
				// Update limiter
					iCotton++;
				// random generate position of Environment
					int iChild = Random.Range(1, tMapHolder.childCount);
				// Loop so that the origin doesn't get spawned on and that the tile that it is being placed on doesnt isnt already obstructed
					while (iOrigins.Contains(iChild) == true || tMapHolder.GetChild(iChild).GetComponent<Tile>().bObstructed == true)
						iChild = Random.Range(1, tMapHolder.childCount);
				// Update Spawn Offset
					lSpawnOffset.Add(iChild);
				// Get Tile of randomized location
					Transform tPlacement = tMapHolder.GetChild(iChild).transform;
				// Instantiate new Cotton
					Transform tCotton = Instantiate(rValue.lPrefabs.ToArray()[4], tPlacement.position, Quaternion.identity) as Transform;
				// Assign Parent Object
					tCotton.parent = tEnvironmentHolder;
				// Parse Reference Core
					tCotton.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Cotton");
				// Update Tile
					tPlacement.GetComponent<Tile>().SetTile(rCore);
				}
			}
		}
	}

// Get the vector size of the island generated
	public Vector2 GetIslandSize() {
		return v2Size;
	}

// Spawn Raft Functions
	public void SpawnRaft(Vector3 position) {
	// Instantiate new Raft
		Transform tRaft = Instantiate(rValue.lPrefabs.ToArray()[5], position, Quaternion.identity) as Transform;
	// Assign Parent Object
		tRaft.parent = tPlacementHolder;
	}

// Spawn Environment Tile
	public bool SpawnEnvironment(string node, Transform parent) {
		if (node == "Berry") {
			// Instantiate new Berry
				Transform tBerry = Instantiate(rValue.lPrefabs.ToArray()[3], parent) as Transform;
			// Assign Parent Object
				tBerry.parent = tEnvironmentHolder;
			// Parse Reference Core
				tBerry.GetComponent<Environment>().SetData(rCore, parent.GetComponent<Tile>(), "Berry");
			// Update Tile
				parent.GetComponent<Tile>().SetTile(rCore);
			// Reduce Energy
				rPlayerStatus.Action(2);
			// Return Complete
				return true;
		}
		else if (node == "Cotton") {
			// Instantiate new Cotton
				Transform tCotton = Instantiate(rValue.lPrefabs.ToArray()[4], parent) as Transform;
			// Assign Parent Object
				tCotton.parent = tEnvironmentHolder;
			// Parse Reference Core
				tCotton.GetComponent<Environment>().SetData(rCore, parent.GetComponent<Tile>(), "Cotton");
			// Update Tile
				parent.GetComponent<Tile>().SetTile(rCore);
			// Reduce Energy
				rPlayerStatus.Action(2);
			// Return Complete
				return true;
		}
	// If no match
		else return false;

	}

// Spawn Placables
	public bool SpawnPlaceable(string node, Transform parent) {
		if (node == "Fireplace") {
			// Instantiate new Fireplace
				Transform tFireplace = Instantiate(rValue.lPrefabs.ToArray()[6], parent) as Transform;
			// Assign Parent Object
				tFireplace.parent = tPlacementHolder;
			// Rename Transform
				tFireplace.name = "Fireplace";
			// Update Tile
				parent.GetComponent<Tile>().SetTile(rCore);
			// Reduce Energy
				rPlayerStatus.Action(2);
			// Return Complete
				return true;
		}
		else if (node == "Tent") {
		// Check if it is in the valid columns
			if ((0 < parent.GetComponent<Tile>().GetNumber() / 10) && (parent.GetComponent<Tile>().GetNumber() / 10 < v2Size.x - 1)) {
			// Check if it is in the valid rows
				if ((0 < parent.GetComponent<Tile>().GetNumber() % 10) && (parent.GetComponent<Tile>().GetNumber() % 10 < v2Size.x - 1)) {
				// Instantiate new Tent
					Transform tTent = Instantiate(rValue.lPrefabs.ToArray()[7], parent) as Transform;
				// Assign Parent Object
					tTent.parent = tPlacementHolder;
				// Update Tile
					parent.GetComponent<Tile>().SetTile(rCore);
				// Get Tile Number;
					int iCentral = parent.GetComponent<Tile>().GetNumber();
					iTent = new int[] { (iCentral - (int)(v2Size.x - 1)), 
										(iCentral - (int)(v2Size.x)),
										(iCentral - (int)(v2Size.x + 1)),
										(iCentral - 1),
										(iCentral + 1),
										(iCentral + (int)(v2Size.x - 1)),
										(iCentral + (int)(v2Size.x)),
										(iCentral + (int)(v2Size.x + 1))};
				// Obstruct rhw area around the Tent spawn
					for (int child = 0; child < iTent.Length; child++)
						tMapHolder.GetChild(iTent[child]).GetComponent<Tile>().SetTile(rCore);
				// Reduce Energy
					rPlayerStatus.Action(2);
				// Return Complete
					return true;
				}
			// Return Failed
				else return false;
			}
		// Return Failed
			else return false;
		}
	// If no match
		else return false;
	}

// Spawn Fire
	public bool SpawnFire(Transform parent) {
	// Check if the Tile is already on fire or not
		if (parent.GetComponent<Tile>().bFire != true) {
		// Instantiate new Fire
			Transform tFire = Instantiate(rValue.lPrefabs.ToArray()[8], parent) as Transform;
		// Assign Parent Object
			tFire.parent = tPlacementHolder;
		// Update Tile
			parent.GetComponent<Tile>().SetTile(rCore);
		// Update on Fire status
			parent.GetComponent<Tile>().bFire = true;
		// Return Complete
			return true;
		}
	// Return Failed
		else return false;
	}

// Get the holder transforms
	public Transform GetHolder(int type) {
		switch (type) {
			case 0: return tMapHolder;
			case 1: return tEnvironmentHolder;
			case 2: return tPlacementHolder;
			default: rCore.Pnt("Missing Information: Missing Holder Type!"); return null;
		}
	}

// Offset the spawn values for map generation
	public void GatherOffset(string type) {
	// Tree
		if (type == "Wood")
			iTree -= 1;
	// Stone
		if (type == "Stone")
			iStone -= 1;
	// Berry Bush
		if (type == "Berry")
			iBerry -= 1;
	// Cotton Bush
		if (type == "Cotton")
			iCotton -= 1;
	}
}
