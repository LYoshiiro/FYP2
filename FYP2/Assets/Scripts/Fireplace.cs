using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;

// Modifier
	[SerializeField] private float fTime;
	[SerializeField] private int iDays;

	private void OnEnable() {
	// Set the timescale to match real time
		Time.timeScale = 1.0f;
	// Set Initial Values
		fTime = 0;
		iDays = 0;
	}

	private void FixedUpdate() {
	// Check if game is paused
        if (rCore.bPause != true) {
			fTime += Time.deltaTime;

		// Check if Fireplace has reached it's lifespan limit
			if (iDays == 3) {
			// Despawn Object
				Despawn();
			}
			else {
				if (fTime > 150.0f) {
				// Update Day
					iDays += 1;
				// Reset Time
					fTime = 0.0f;
				}
			}
		}
	}

// Despawn Function
	public void Despawn() {
	// Hide Updating GameObject
		transform.GetComponentInChildren<MeshRenderer>().enabled = false;
	// Destroy old GameObject
		Destroy(transform.gameObject);
	}

// Set the Core Reference for Fireplace
	public void SetCore(Core core) {
		rCore = core;
	}
}
