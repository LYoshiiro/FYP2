using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
// Class Reference
    [SerializeField] private Core rCore;
    [SerializeField] private PlayerCamera rPCamera;

// Facing Direction
	private RaycastHit hit;
	private Ray ray;

// Interaction
    private float fBounceTime;

    private void FixedUpdate() {
 // Check if game is paused
        if (rCore.bPause != true) {
        // Set Ray to fire from camera to mouse
			ray = rPCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

			// Get RaycastHit Point in the world space
			if (Physics.Raycast(ray, out hit, 100f)) {
				Vector3 v3Look = hit.point - transform.position;
				// Lock the Y so that it doesnt look downwards
				v3Look.y = 0;
				Quaternion qRotation = Quaternion.LookRotation(v3Look);
				// Turn Player to the cursor point
				transform.GetChild(1).transform.rotation = qRotation;

            }
        }
    }
}
