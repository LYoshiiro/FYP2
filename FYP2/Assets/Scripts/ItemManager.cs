using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInteraction rPlayer;

// Parse Values
	private string sFile;
	private string[] sAry;

// Management List
	private List<Item> lItem;

// Tools Prefab
	[SerializeField] private List<Transform> lTools;

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
		// Assess if the item can be used
			bool bEat = (sSplit[2] == "yes") ? true : false;
			bool bUse = (sSplit[3] == "yes") ? true : false;
			lItem.Add(new Item(int.Parse(sSplit[0]), int.Parse(sSplit[1]), bEat, bUse, sSplit[4], sSplit[5]));
		}
	}

// Gathering Function
	public void Gather(string name) {
	// Get Reference
		Item rGather = lItem.Find(i => i.sName == name);
	// Update Count
		// rGather.CountUpdate(Random.Range(2, 5));
	// Debug Scenario
		rGather.CountUpdate(Random.Range(12, 15));
	}

// Parse item list
	public List<Item> GetItems() {
		return lItem;
	}

// Spawn Tool based on value
	public void SpawnTool(string type) {
	// Axe
		if (type == "Wood") 		{ Transform tAxe = Instantiate(lTools.ToArray()[0], rPlayer.transform) as Transform; tAxe.name = "Axe"; }
	// Pickaxe
		else if (type == "Stone") 	{ Transform tAxe = Instantiate(lTools.ToArray()[2], rPlayer.transform) as Transform; tAxe.name = "Pickaxe"; }
	// Hoe
		else if (type == "Cotton") 	{ Transform tAxe = Instantiate(lTools.ToArray()[1], rPlayer.transform) as Transform; tAxe.name = "Hoe"; }
	// Hoe
		else if (type == "Berry") 	{ Transform tAxe = Instantiate(lTools.ToArray()[1], rPlayer.transform) as Transform; tAxe.name = "Hoe"; }
	}

// Despawn Tool
	public void DespawnTool() {
		foreach (Transform child in rPlayer.transform)
			GameObject.DestroyImmediate(child.gameObject);
	}
}
