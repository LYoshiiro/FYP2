using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ItemManager rItemManager;
	[SerializeField] private Transform rMenu;

	private void LateUpdate() {
	// Check if game is paused
        if (rCore.bPause != true) {
			if (Input.GetKeyDown(KeyCode.C))
				rMenu.gameObject.SetActive(!rMenu.gameObject.activeSelf);
		}
	}

// Crafting Raft Function
	public void CraftRaft() {
	// Check cost
		if (rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 12) {
		// Reduce cost
			rItemManager.GetItems().Find(i => i.sName == "Wood").iCount -= 12;
		// Add item
			rItemManager.GetItems().Find(i => i.sName == "Raft").iCount += 1;
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
					rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount -= 3;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Hat").iCount += 1;
				}
			break;
		
		// Shirt
			case 1:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 5) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount -= 5;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Shirt").iCount += 1;
				}
			break;
		
		// Pant
			case 2:
			// Check cost
				if (rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount >= 5) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Cotton").iCount -= 5;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Cotton Pant").iCount += 1;
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
					rItemManager.GetItems().Find(i => i.sName == "Wood").iCount -= 2;
					rItemManager.GetItems().Find(i => i.sName == "Stone").iCount -= 2;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Hoe").iCount += 1;
				}
			break;
		
		// Axe
			case 1:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 2) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 3)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").iCount -= 2;
					rItemManager.GetItems().Find(i => i.sName == "Stone").iCount -= 3;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Axe").iCount += 1;
				}
			break;
		
		// Pickaxe
			case 2:
			// Check cost
				if ((rItemManager.GetItems().Find(i => i.sName == "Wood").iCount >= 2) &&
					(rItemManager.GetItems().Find(i => i.sName == "Stone").iCount >= 3)) {
				// Reduce cost
					rItemManager.GetItems().Find(i => i.sName == "Wood").iCount -= 2;
					rItemManager.GetItems().Find(i => i.sName == "Stone").iCount -= 3;
				// Add item
					rItemManager.GetItems().Find(i => i.sName == "Pickaxe").iCount += 1;
				}
			break;
			
		// Irregularity
			default:
				rCore.Pnt("No call value!");
			break;
		}
	}
}
