using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapValue : MonoBehaviour {
// Map Data Values
	[SerializeField] public int iTree;
	[SerializeField] public int iStone;
	[SerializeField] public int iBerry;
	[SerializeField] public int iCotton;

// Map Prefabs
	[SerializeField] public List<Transform> lPrefabs;

	private void Start() {
		// Generate Initial Values
		iTree = Random.Range(3, 5);
		iStone = Random.Range(3, 5);
		iBerry = Random.Range(1, 3);
		iCotton = Random.Range(1, 3);
	}
}
