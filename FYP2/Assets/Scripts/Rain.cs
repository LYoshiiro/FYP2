using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ParticleSystem rParticleSystem;

	private void OnEnable() {
	// Attach reference to Particle Emission
		var vEmission = rParticleSystem.emission;
	// Reset RoT
		vEmission.rateOverTime = 500;
	// Attach reference to Particle shape
		var vShape = rParticleSystem.shape;
	// Generate Wind Origin
		int iDisplacement = Random.Range(-1, 2);
	// Displace the Original Spawn location
		vShape.position = new Vector3(iDisplacement, 0, 0);
	// Attach reference to Particle force over life
		var vForce = rParticleSystem.forceOverLifetime;
	// Displace x-axis
		vForce.x = -iDisplacement;
	}

	private void FixedUpdate() {
	// Gradually lessen the rain drops
		if (rCore.fTime > 120.0f) {
		// Get the remainding time as percentage
			float fRemainder = 150.0f - rCore.fTime;
			float fTimeToEnd = fRemainder / 30.0f;
		// Attach reference to Particle Emission
			var vEmission = rParticleSystem.emission;
		// Modify as time comes to an end
			vEmission.rateOverTime = 500 * fTimeToEnd;
		}
	}
}
