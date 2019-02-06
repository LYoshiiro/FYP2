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
	public void Freezing(int value) {
	// Hidden Addition length for kill pop-up
		if (iCold < 15)
			iCold += value;

	// Kill
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
	public void Rest(int value) {
	// Increase Levels
		iEnergy += value;
	// Limiter
		if (iEnergy > 10)
			iEnergy = 10;
	}

// Decrease Energy
	public void Action(int value) {
	// Hidden Addition length for kill pop-up
		if (iEnergy > -5)
			iEnergy -= value;
	// Kill
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

// Set value to zero
	public void ClampStatus(int type, int value) {
		switch (type) {
			case 0: iCold = value; break;
			case 1: iEnergy = value; break;
			default: rCore.Pnt("Missing Info: Clamp Type Missing!"); break;
		}
	}
}
