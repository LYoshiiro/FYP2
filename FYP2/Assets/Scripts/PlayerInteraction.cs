using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
// Class Reference
    [SerializeField] private Core rCore;
    [SerializeField] private PlayerCamera rPlayerCamera;
	[SerializeField] private ItemManager rItemManager;

// Facing Direction
	private RaycastHit hit;
	private Ray ray;

// Interaction
    private float fBounceTime;

    private void FixedUpdate() {
 // Check if game is paused
        if (rCore.bPause != true) {
		// // If game isn't paused, update elapse time
		// 	fBounceTime += Time.deltaTime;
        // Set Ray to fire from camera to mouse
			ray = rPlayerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

		// Get RaycastHit Point in the world space
			if (Physics.Raycast(ray, out hit, 100f)) {
				Vector3 v3Look = hit.point - transform.position;
			// Lock the Y so that it doesnt look downwards
				v3Look.y = 0;
				Quaternion qRotation = Quaternion.LookRotation(v3Look);
			// Turn Player to the cursor point
				transform.GetChild(0).transform.rotation = qRotation;

			// When LMB is held down
				if (Input.GetMouseButton(0)) {
				// Check Bounce Timer
					if (fBounceTime > 1.5f) {

					// Check if hit object has environment class
						if (hit.transform.GetComponentInParent<Environment>() != null) {
						// Display the distance between the gathering object and the player
                            // rCore.Pnt(Vector3.Magnitude((transform.position - hit.transform.position)));

						// Check if the distance between the player and the environment object is 'close'
							if (Vector3.Magnitude(transform.position - hit.transform.position) < 1.25f) {
							// Attach a reference transform to the hit object
								Transform tGather = hit.transform;
							// Check if the Item Manager is working or not
								if (rItemManager != null) {
									// Parse the material just gathered and update the datalist
									rItemManager.Gather(tGather.GetComponentInParent<Environment>().sNode);

									// Remove environment object
									tGather.GetComponentInParent<Environment>().Despawn();
								}
								else
									rCore.Pnt("Missing Item Manager!");
							}
						}
					// Reset Bounce Timer
						fBounceTime = 0.0f;
					}
					else
					// Update Hold Time
						fBounceTime += Time.deltaTime;
				}
            }
        }
    }
}
