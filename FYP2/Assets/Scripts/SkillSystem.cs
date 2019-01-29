using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInventory rPlayerInventory;
	[SerializeField] private Crafting rCrafting;
	[SerializeField] private Transform tMenu;

// Leveling
	[SerializeField] private float fExperience;
	[SerializeField] private float fProgressive;
	[SerializeField] private List<Text> lText;

// Catagories
	private int iGLevel; // Gathering 	Level
	private int iLLevel; // Lumbering 	Level
	private int iMLevel; // Mining 		Level
//===============================================
	private int iGSpeed; // Gathering 	Speed
	private int iLSpeed; // Lumbering 	Speed
	private int iMSpeed; // Mining 		Speed
//===============================================
	private int iGAmount; // Gathering 	Amount
	private int iLAmount; // Lumbering 	Amount
	private int iMAmount; // Mining 	Amount

	private void Start() {
	// Set Initial Values
		fProgressive = 25.0f;
	}

	private void FixedUpdate() {
	// If key is called
		if (Input.GetKeyDown(KeyCode.H)) {
		// Close other Menus
			rCrafting.CloseMenu();
			rPlayerInventory.CloseMenu();
			tMenu.gameObject.SetActive(!tMenu.gameObject.activeSelf);
		}
		else
		{
			lText.ToArray()[0].text = iGSpeed.ToString();	// Update UI
			lText.ToArray()[1].text = iLSpeed.ToString();	// Update UI
			lText.ToArray()[2].text = iMSpeed.ToString();	// Update UI
			lText.ToArray()[3].text = iGAmount.ToString();	// Update UI
			lText.ToArray()[4].text = iLAmount.ToString();	// Update UI
			lText.ToArray()[5].text = iMAmount.ToString();	// Update UI
			lText.ToArray()[6].text = iGLevel.ToString();	// Update UI
			lText.ToArray()[7].text = iLLevel.ToString();	// Update UI
			lText.ToArray()[8].text = iMLevel.ToString();	// Update UI

			lText.ToArray()[9].text = "Level: " + Mathf.Clamp(fExperience, 0, fProgressive) + " / " + fProgressive;
			lText.ToArray()[10].text = "Experience: "+ fExperience;
		}
	}

// Function to gain Experience
	public void IncreaseExperience(int value) {
		fExperience += value;
	}

// Function to increase Skills
	public void IncreaseLevel(int type) {
	// Check if Level reach mark was hit
		if (fExperience >= fProgressive) {
		// Find type of level up
			switch (type) {
			// Speed
			// Limit Level
				case 0: if (iGSpeed < 3) {
						iGSpeed += 1;					// Increase Level
						iGLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Gathering
				case 1: if (iLSpeed < 3) {
						iLSpeed += 1;					// Increase Level
						iLLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Lumbering
				case 2: if (iMSpeed < 3) {
						iMSpeed += 1;					// Increase Level
						iMLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Mining
			// Amount
				case 3: if (iGAmount < 3) {
						iGAmount += 1;					// Increase Level
						iGLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Gathering
				case 4: if (iLAmount < 3) {
						iLAmount += 1;					// Increase Level
						iLLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Lumbering
				case 5: if (iMAmount < 3) {
						iMAmount += 1;					// Increase Level
						iMLevel += 1;					// Increase Max
						fExperience -= fProgressive;	// Reduce Cost from pool
						fProgressive *= 2;				// Increase Cost
						} break; // Mining
			// Irregularity
				default: rCore.Pnt("Missing Info: Level Type!"); break;
			}
		}
	}

// Get Type's Level
	public int GetLevel(int type) {
		switch (type) {
			case 0: return iGSpeed;
			case 1: return iLSpeed;
			case 2: return iMSpeed;
			case 3: return iGAmount;
			case 4: return iLAmount;
			case 5: return iMAmount;
			default: rCore.Pnt("Missing Info: Level Type!"); return 0;
		}
	}

// Close the Inventory Menu
	public void CloseMenu() {
		tMenu.gameObject.SetActive(false);
	}
}
