using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
// References
	[SerializeField] private Core rCore;
	[SerializeField] private Transform tPlayer;
	[SerializeField] private Transform tCamera;
	private Camera cCamera;

// Type
	[SerializeField] private int iType;

// Modifier
	[SerializeField] private float fDistance;
	[SerializeField] private float fCurrentX;
	[SerializeField] private float fCurrentY;
	[SerializeField] private float fSensitivityX;
	[SerializeField] private float fSensitivityY;
	[SerializeField] private float fMinY;
	[SerializeField] private float fMaxY;
	[SerializeField] private Vector3 v3Direction;
	[SerializeField] private Quaternion qRotation;

// Manupulators
	private Vector2 v3Start;
	private Vector2 v3End;


	private void Start() {
	// Do initial assignment
		tCamera = transform;
		cCamera = transform.GetComponent<Camera>();
	}

	private void FixedUpdate() {
		switch (iType) {
			case 0:
			// Check if game is paused
				if (!rCore.bPause) {
				// When LMB is held down
					if (Input.GetMouseButton(1)) {
						fCurrentX += Input.GetAxis("Mouse X") * fSensitivityX;
						fCurrentY += Input.GetAxis("Mouse Y") * fSensitivityX;

						fCurrentY = Mathf.Clamp(fCurrentY, fMinY, fMaxY);
					}
				}
			break;
			case 1:
				fCurrentX += Time.deltaTime * fSensitivityX;
			break;
			default: rCore.Pnt("Missing Info: Missing Type!"); break;
		}
	}

	private void LateUpdate() {
		switch (iType) {
			case 0:
			// Set Distance away from the player
				v3Direction = new Vector3(0, 0, -fDistance);
			// Set rotation around player
				qRotation = Quaternion.Euler(fCurrentY, fCurrentX, 0);
			// Set Camera away from player at rotation
				tCamera.position = tPlayer.position + qRotation * v3Direction;
			// Look at the player
				tCamera.LookAt(tPlayer.position);
			break;
			case 1:
			// Set Distance away from the origin
				v3Direction = new Vector3(0, 0, -fDistance);
			// Set rotation around origin
				qRotation = Quaternion.Euler(fCurrentY, fCurrentX, 0);
			// Set Camera away from origin at rotation
				tCamera.position = Vector3.zero + qRotation * v3Direction;
			// Look at the origin
				tCamera.LookAt(Vector3.zero);
			break;
			default: rCore.Pnt("Missing Info: Missing Type!"); break;
		}
	}
}
