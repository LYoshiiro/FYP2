using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapValue : MonoBehaviour {
// Map Data Values
	[SerializeField] public int iTree;
	[SerializeField] public int iStone;
	[SerializeField] public int iBush;

// Map Prefabs
	[SerializeField] public List<Transform> lPrefabs;

	private void Start() {
		// Generate Initial Values
		iTree = Random.Range(3, 5);
		iStone = Random.Range(3, 5);
		iBush = Random.Range(2, 4);
	}
}
