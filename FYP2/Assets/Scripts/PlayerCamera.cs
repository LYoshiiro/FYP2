using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
// References
	[SerializeField] private Core rCore;
	[SerializeField] private Transform tPlayer;
	[SerializeField] private Transform tCamera;
	private Camera cCamera;

// Modifier
	[SerializeField] private float fDistance;
	[SerializeField] private float fCurrentX;
	[SerializeField] private float fCurrentY;
	[SerializeField] private float fSensitivityX;
	[SerializeField] private float fSensitivityY;
	[SerializeField] private float fMinY;
	[SerializeField] private float fMaxY;

// Manupulators
	private Vector2 v3Start;
	private Vector2 v3End;


	private void Start() {
	// Do initial assignment
		tCamera = transform;
		cCamera = transform.GetComponent<Camera>();
	}

	private void Update() {
    // Check if game is paused
		if (!rCore.bPause) {
		// When LMB is held down
			if (Input.GetMouseButton(1)) {
				fCurrentX += Input.GetAxis("Mouse X") * fSensitivityX;
				fCurrentY += Input.GetAxis("Mouse Y") * fSensitivityX;

				fCurrentY = Mathf.Clamp(fCurrentY, fMinY, fMaxY);
			}
		}
	}

	private void LateUpdate() {
	// Set Distance away from the player
		Vector3 v3Direction = new Vector3(0, 0, -fDistance);
	// Set rotation around player
		Quaternion qRotation = Quaternion.Euler(fCurrentY, fCurrentX, 0);
	// Set Camera away from player at rotation
		tCamera.position = tPlayer.position + qRotation * v3Direction;
	// Look at the player
		tCamera.LookAt(tPlayer.position);
	}
}
