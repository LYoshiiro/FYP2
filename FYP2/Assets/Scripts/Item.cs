using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
// Class Reference
	[SerializeField] private Core rCore;

// Data Values
	[SerializeField] public int iID;
	[SerializeField] public int iCount;
	[SerializeField] public bool bEat;
	[SerializeField] public bool bUse;
	[SerializeField] public string sName;
	[SerializeField] public string sNote;

// Create Function
	public Item(int id, int count, bool eat, bool use, string name, string note) {
		iID 	= id;		// Item ID
		iCount  = count;	// Item Count in Inventory
		bEat 	= eat;		// Eatable
		bUse 	= use;		// Usable
		sName 	= name;		// Item Name
		sNote 	= note;		// Item Flavor Text
	}

// Item Count Updater
	public void CountUpdate(int value) {
	// Updater
		iCount += value;
	// Safe Net
		if (iCount < 0)
			iCount = 0;
	}
}
