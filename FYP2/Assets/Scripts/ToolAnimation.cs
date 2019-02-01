using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAnimation : MonoBehaviour {
// Modifier
	[SerializeField] private float fShift;

	private void FixedUpdate() {
	// Animate the tool to go up and down
		fShift = Mathf.Sin(Time.time * 20.0f);
		transform.Rotate(Vector3.forward, fShift);
	}
}
