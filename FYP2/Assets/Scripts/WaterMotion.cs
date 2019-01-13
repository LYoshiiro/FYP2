using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMotion : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;

// Motion Data Value
	[SerializeField] private Vector3 v3Origin;
	[SerializeField] private float fShift;
	[SerializeField] private float fDivision;

	private void Start() {
		v3Origin = transform.position;
	}

	private void FixedUpdate() {
    // Check if game is paused
        if (rCore.bPause != true)
		// Water Motion
			transform.position = v3Origin + (transform.up * (Mathf.Sin(Time.time * fShift)) / fDivision);
	}

}
