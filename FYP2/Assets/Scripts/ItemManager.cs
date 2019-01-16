using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;

// Parse Values
	private string sFile;
	private string[] sAry;

// Management List
	private List<Item> lItem;

	private void Start() {
	// Create a new Item list
		lItem = new List<Item>();

	// Create File Path
		sFile = "Item.txt";

	// Get Content from File Path
		sAry = rCore.CSVRead(sFile);

	// Instantiate all items from the File Path
		for (int i = 0; i < sAry.Length; i++) {
		// Split the take in line as seperated values
			string[] sSplit = sAry[i].Split(',');
			lItem.Add(new Item(int.Parse(sSplit[0]), int.Parse(sSplit[1]), sSplit[2], sSplit[3]));
		}
	}

// Gathering Function
	public void Gather(string name) {
	// Get Reference
		Item rGather = lItem.Find(i => i.sName == name);
		rGather.CountUpdate(Random.Range(3, 6));
	}

// Parse item list
	public List<Item> GetItems() {
		return lItem;
	}
}
