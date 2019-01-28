using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private Item rItem;
	[SerializeField] private PlayerInventory rPlayer;

// Set Reference
	public void SetReference(Core core, Item item, PlayerInventory player, int type) {
		rCore = core;
		rItem = item;
		rPlayer = player;
		switch (type) {
		// Use Button
			case 0:
			if (rItem.bUse)
			transform.gameObject.SetActive(rItem.bUse);
			GetComponent<Button>().onClick.AddListener(Use); break;
		// Eat Button
			case 1:
			if (rItem.bEat) 
			transform.gameObject.SetActive(rItem.bEat);
			GetComponent<Button>().onClick.AddListener(Eat); break;
		// Irregularity
			default: rCore.Pnt("Missing Infomation: Button Type!"); break;
		}
		rCore.Pnt(rItem.sName + ", " + rItem.bUse + ", " + rItem.bEat);
	}

// Use Item
	private void Use() {
	// Check for Validity
		if ((rItem.bUse == true) && (rItem.iCount > 0)) {
		// Reduce Count
			rItem.CountUpdate(-1);
		// Update Menu
			rPlayer.bMenuChange = true;
		}
	}

// Eat Item
	private void Eat() {
	// Check for Validity
		if ((rItem.bEat == true) && (rItem.iCount > 0)) {
		// Reduce Count
			rItem.CountUpdate(-1);
		// Update Menu
			rPlayer.bMenuChange = true;
		}
	}
}
