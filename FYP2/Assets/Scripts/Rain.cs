using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ParticleSystem rParticleSystem;

	private void OnEnable() {
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
}
