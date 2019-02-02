using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private MapGenerator rMapGenerator;
// Modifier
	[SerializeField] private Vector3 v3Offset;
	[SerializeField] private Vector3 v3Direction;
	[SerializeField] private float fSpeed;

	private void OnEnable() {
	// Random Values
		int iSpawn = Random.Range(0, 8);
		switch (iSpawn) {
			case 0: v3Offset = new Vector3(-6, 0, -6); v3Direction = new Vector3(1, 0, 0); break;
			case 1: v3Offset = new Vector3(6, 0, -6); v3Direction = new Vector3(0, 0, 1); break;
			case 2: v3Offset = new Vector3(6, 0, 6); v3Direction = new Vector3(-1, 0, 0); break;
			case 3: v3Offset = new Vector3(-6, 0, 6); v3Direction = new Vector3(0, 0, -1); break;

			case 4: v3Offset = new Vector3(6, 0, 6); v3Direction = new Vector3(0, 0, -1); break;
			case 5: v3Offset = new Vector3(6, 0, -6); v3Direction = new Vector3(-1, 0, 0); break;
			case 6: v3Offset = new Vector3(-6, 0, -6); v3Direction = new Vector3(0, 0, 1); break;
			case 7: v3Offset = new Vector3(-6, 0, 6); v3Direction = new Vector3(1, 0, 0); break;
			default: rCore.Pnt("Random out of bounce!"); break;
		}	

	// Offset Position
		transform.position = v3Offset;
	}

	private void FixedUpdate() {
	// Translate Tornado if it hasn't complete its course
		if (Vector3.Magnitude(transform.position - v3Offset) < 15.0f)
			transform.Translate(v3Direction * fSpeed, Space.Self);
	// Turn off the gameobject when completed
		else
			transform.gameObject.SetActive(false);
	}
}
