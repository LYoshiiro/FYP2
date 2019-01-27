using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ItemManager rItemManager;

// Menu Variables
	[SerializeField] private GameObject tItem;
	[SerializeField] private Transform tContent;
	[SerializeField] private Transform tMenu;
	private Dictionary<int, Item> dItems;
	private bool bMenuChange;

	private void Start() {
	// Set Initial Values
		bMenuChange = true;
		dItems = new Dictionary<int, Item>();
	}

	private void LateUpdate() {
		foreach (Item itm in rItemManager.GetItems()) {
		// Check if the Dictionary contains instance
			if (!dItems.ContainsKey(itm.iID)) {
				dItems.Add(itm.iID, itm);
			}
		// Check if the count matches
			else if (dItems.ContainsKey(itm.iID)) {
				if (dItems[itm.iID].iCount != itm.iCount) {
					dItems[itm.iID] = itm;
					bMenuChange = true;
				}
			}
		}

    // Check if game is paused
        if (rCore.bPause != true) {
		// Only Update if there is changes
			if (bMenuChange) {
			// Destroy all children first
				foreach (Transform child in tContent)
					GameObject.Destroy(child.gameObject);

			// Create new Menu list
				for (int i = 0; i < dItems.Count; i++) {
					GameObject gItem = Instantiate(tItem, tContent) as GameObject;
					gItem.name = dItems[i].sName;
					gItem.transform.GetChild(0).GetComponent<Text>().text = "Name: " + dItems[i].sName;
					gItem.transform.GetChild(1).GetComponent<Text>().text = "x" + dItems[i].iCount;
					gItem.transform.GetChild(2).GetComponent<Text>().text = dItems[i].sNote;
				}

			// If key is called
				if (Input.GetKeyDown(KeyCode.I))
					tMenu.gameObject.SetActive(!tMenu.gameObject.activeSelf);
			}
		}
	}
}
