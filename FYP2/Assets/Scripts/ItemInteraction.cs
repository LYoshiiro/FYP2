using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private Item rItem;
	[SerializeField] private PlayerInventory rPlayerInventory;
	[SerializeField] private PlayerInteraction rPlayerInteraction;
	[SerializeField] private PlayerStatus rPlayerStatus;
	[SerializeField] private CursorIndicator rCursorIndicator;

// Set Reference
	public void SetReference(Core core, Item item, PlayerInventory inventory, PlayerInteraction interaction, PlayerStatus status, CursorIndicator cursor, int type) {
		rCore = core;
		rItem = item;
		rPlayerInventory = inventory;
		rPlayerInteraction = interaction;
		rPlayerStatus = status;
		rCursorIndicator = cursor;
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
	}

// Use Item
	private void Use() {
	// Check for Validity
		if ((rItem.bUse == true) && (rItem.iCount > 0)) {
		// Close Menu
			rPlayerInventory.CloseMenu();
		// Attach Reference
			rPlayerInteraction.sPlacing = rItem.sName;
		// Change Cursor
			rCursorIndicator.SetCursor(1);
		}
	}

// Eat Item
	private void Eat() {
	// Check for Validity
		if ((rItem.bEat == true) && (rItem.iCount > 0)) {
		// Reduce Count
			rItem.CountUpdate(-1);
		// Reduce Cold
			rPlayerStatus.Defreeze();
		// Increase Energy
			rPlayerStatus.Rest(1);
		// Update Menu
			rPlayerInventory.bMenuChange = true;
		}
	}
}
