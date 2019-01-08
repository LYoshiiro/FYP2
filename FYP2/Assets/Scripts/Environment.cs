using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
// Class Reference
	[SerializeField] public Core rCore;

// Data Values
	[SerializeField] public string sNode;

// Set data for references
	public void SetData(Core core, string node) {
		rCore = core;
		sNode = node;
	}

	public void Despawn() {
	// Hide Updating GameObject
		transform.GetComponentInChildren<MeshRenderer>().enabled = false;
	// Destroy old GameObject
		Destroy(transform.gameObject);
	}
}
