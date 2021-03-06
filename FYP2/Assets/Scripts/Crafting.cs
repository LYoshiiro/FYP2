﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private UI rUI;
	[SerializeField] private ItemManager rItemManager;
	[SerializeField] private PlayerInventory rPlayerInventory;
	[SerializeField] private SkillSystem rSkillSystem;
	[SerializeField] private Transform tMenu;

	private void LateUpdate() {
	// Check if game is paused
        if (rCore.bPause != true) {
			if (Input.GetKeyDown(KeyCode.C)) {
			// Close other Menus
				rPlayerInventory.CloseMenu();
				rSkillSystem.CloseMenu();
				tMenu.gameObject.SetActive(!tMenu.gameObject.activeSelf);
				rUI.SetIndicator("Crafting!", (tMenu.gameObject.activeSelf == true)? 1 : 0);
			}
		}
	}

// Close the Inventory Menu
	public void CloseMenu() {
		tMenu.gameObject.SetActive(false);
		rUI.SetIndicator(string.Empty, 0);
	} 

// Crafting Raft Function
	public void CraftRaft() {
	// Check cost
		if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 12) &&
			(rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 8)){
		// Reduce cost
			rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-12);
			rItemManager.GetItems().Find(i => i.sName == "Cotton").CountUpdate(-8);
		// Add item
			rItemManager.GetItems().Find(i => i.sName == "Raft").CountUpdate(1);
		// Update Menu
			rPlayerInventory.bMenuChange = true;
		// Update Indicator
			rUI.SetIndicator("Raft x1", 3);
		}
	}

// Clothing
	public void CraftClothes(int call) {
	// Check which section is being called
		switch (call) {
		// Hat
			case 0:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 3) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Cotton").CountUpdate(-3);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Hat").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Cotton Hat x1", 3);
			}
			break;
		
		// Shirt
			case 1:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 5) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Cotton").CountUpdate(-5);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Shirt").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Cotton Shirt x1", 3);
			}
			break;
		
		// Pant
			case 2:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 5) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Cotton").CountUpdate(-5);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Pant").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Cotton Pant x1", 3);
			}
			break;
			
		// Irregularity
			default:
				rCore.Pnt("No call value!");
			break;
		}
	}

// Clothing
	public void CraftTools(int call) {
	// Check which section is being called
		switch (call) {
		// Hoe
			case 0:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 2) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 2)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-2);
					rItemManager.GetItems().Find(i => i.sName == "Stone").CountUpdate(-2);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Hoe").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Hoe x1", 3);
			}
			break;
		
		// Axe
			case 1:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 2) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 3)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-2);
					rItemManager.GetItems().Find(i => i.sName == "Stone").CountUpdate(-3);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Axe").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Axe x1", 3);
			}
			break;
		
		// Pickaxe
			case 2:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 2) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 3)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-2);
					rItemManager.GetItems().Find(i => i.sName == "Stone").CountUpdate(-3);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Pickaxe").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Pickaxe x1", 3);
			}
			break;
			
		// Bucket
			case 3:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 3) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-3);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Bucket").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Bucket x1", 3);
			}
			break;

		// Irregularity
			default:
				rCore.Pnt("No call value!");
			break;
		}
	}

// Fireplace & Tent
	public void CraftStructures(int call) {
	// Check which section is being called
		switch (call) {
		// Fireplace
			case 0:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 3) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 3)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-3);
					rItemManager.GetItems().Find(i => i.sName == "Stone").CountUpdate(-3);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Fireplace").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Fireplace x1", 3);
			}
			break;
		// Tent
			case 1:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 7) &&
					(rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 7)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").CountUpdate(-7);
					rItemManager.GetItems().Find(i => i.sName == "Cotton").CountUpdate(-7);
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Tent").CountUpdate(1);
				// Update Menu
					rPlayerInventory.bMenuChange = true;
				// Update Indicator
					rUI.SetIndicator("Tent x1", 3);
			}
			break;
		// Irregularity
			default:
				rCore.Pnt("No call value!");
			break;
		}
	}

}
