using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapValue : MonoBehaviour {
// Map Data Values
	[SerializeField] private int iTree;
	[SerializeField] private int iStone;
	[SerializeField] private int iBush;

// Map Prefabs
	[SerializeField] private List<Transform> lPrefabs;

// Parse Prefab list
	public List<Transform> ParseList() {
		return lPrefabs;
	}
}
