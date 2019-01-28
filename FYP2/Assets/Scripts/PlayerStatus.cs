using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private UI rUI;

// Modifiers
	[SerializeField] private int iCold;
	[SerializeField] private int iEnergy;

	private void Start() {
	// Set Initial Values
		iCold = 3;		// 3  / 10
		iEnergy = 10;	// 10 / 10
	}

// Increase Cold
	public void Freezing() {
	// Hidden Addition length for kill pop-up
		if (iCold < 15)
			iCold++;

		if (iCold >= 15)
			rUI.ColdDeath();
	}

// Decrease Cold
	public void Defreeze() {
	// Reduce Levels
		iCold--;
	// Limiter
		if (iCold < 0)
			iCold = 0;
	}

// Increase Energy
	public void Rest() {
	// Increase Levels
		iEnergy++;
	// Limiter
		if (iEnergy > 10)
			iEnergy = 10;
	}

// Decrease Energy
	public void Action() {
	// Hidden Addition length for kill pop-up
		if (iEnergy > -5)
			iEnergy--;
	// Limiter
		if (iEnergy <= -5)
			rUI.StarveDeath();
	}

// Get Status Values
	public int GetStatus(int type) {
		switch (type) {
			case 0: return iCold;
			case 1: return iEnergy;
			default: rCore.Pnt("Missing Info: Status Type!"); return 0;
		}
	}
}
