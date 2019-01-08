using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
// Reference Player
	[SerializeField] private Transform tPlayer;

// Origin Distance
	[SerializeField] private Vector3 v3Dist;

	private void Start() {
		v3Dist = transform.position;
	}

	private void FixedUpdate() {
		transform.position = tPlayer.position + v3Dist;
	}
}
