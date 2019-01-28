using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ItemManager rItemManager;
	[SerializeField] private PlayerInteraction rPlayerInteraction;
	[SerializeField] private PlayerStatus rPlayerStatus;
	[SerializeField] private Crafting rCrafting;
	[SerializeField] private SkillSystem rSkillSystem;

// Menu Variables
	[SerializeField] private GameObject tItem;
	[SerializeField] private Transform tContent;
	[SerializeField] private Transform tMenu;
	public bool bMenuChange;

	private void Start() {
	// Set Initial Values
		bMenuChange = true;
	}

	private void LateUpdate() {
	// Update Menu when player interacts
		if (rPlayerInteraction.bProgressBar)
			bMenuChange = true;

    // Check if game is paused
        if (rCore.bPause != true) {
		// Only Update if there is changes
			if (bMenuChange == true) {
			// Destroy all children first
				foreach (Transform child in tContent)
					GameObject.Destroy(child.gameObject);

			// Create new Menu list
				for (int i = 0; i < rItemManager.GetItems().ToArray().Length; i++) {
					GameObject gItem = Instantiate(tItem, tContent) as GameObject;
				// Give Instance a name
					gItem.name = rItemManager.GetItems().ToArray()[i].sName;
				// Update Visual Details
					gItem.transform.GetChild(0).GetComponent<Text>().text = "Name: " + rItemManager.GetItems().ToArray()[i].sName;
					gItem.transform.GetChild(1).GetComponent<Text>().text = "x" + rItemManager.GetItems().ToArray()[i].iCount;
					gItem.transform.GetChild(2).GetComponent<Text>().text = rItemManager.GetItems().ToArray()[i].sNote;
				// Check usability before setting button
					gItem.transform.GetChild(3).GetComponent<ItemInteraction>().SetReference(rCore, rItemManager.GetItems().ToArray()[i], GetComponent<PlayerInventory>(), rPlayerInteraction, rPlayerStatus, 0);
				// Assign Reference Function
					gItem.transform.GetChild(4).GetComponent<ItemInteraction>().SetReference(rCore, rItemManager.GetItems().ToArray()[i], GetComponent<PlayerInventory>(), rPlayerInteraction, rPlayerStatus, 1);
				}
			// Hook for Visual Data Update
				bMenuChange = false;
			}
		// If key is called
			if (Input.GetKeyDown(KeyCode.I)) {
			// Close other Menus
				rCrafting.CloseMenu();
				rSkillSystem.CloseMenu();
				tMenu.gameObject.SetActive(!tMenu.gameObject.activeSelf);
			}
		}
	}

// Close the Inventory Menu
	public void CloseMenu() {
		tMenu.gameObject.SetActive(false);
	}
}
