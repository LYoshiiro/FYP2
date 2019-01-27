using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
// Class References
	[SerializeField] private Core rCore;
	[SerializeField] private MapValue rValue;

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
	private int iTree;
	private int iStone;
	private int iBerry;
	private int iCotton;
	private int iTile;
	private List<int> lSpawnOffset;

	private void Start() {
	// Set Initial Variables
		iTree = 0;
		iStone = 0;
		iBerry = 0;
		iCotton = 0;
		iTile = 0;
		lSpawnOffset = new List<int>();

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
		GenerateEnvironment();
	}

// Generate Environment Objects
	private void GenerateEnvironment() {
	// Check the holder for avaliability
		if (tMapHolder != null) {
		// Check if limit is reached
		// Tree =========================================================
			if (iTree < rValue.iTree) {
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
			// Parse Reference Core
				tCotton.GetComponent<Environment>().SetData(rCore, tPlacement.GetComponent<Tile>(), "Cotton");
			// Update Tile
				tPlacement.GetComponent<Tile>().SetTile(rCore);
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
}
