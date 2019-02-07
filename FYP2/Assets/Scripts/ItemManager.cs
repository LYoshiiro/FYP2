using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private PlayerInteraction rPlayer;
	[SerializeField] private MapGenerator rMapGenerator;
	[SerializeField] private SkillSystem rSkillSystem;

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
	// Offset Generated Values
		rMapGenerator.GatherOffset(name);
	// Update Count
		rGather.CountUpdate(Random.Range(2 + ItemModify(name), 5 + ItemModify(name)));
		// rGather.CountUpdate(5 + ItemModify(name));
	// Debug Scenario
		// rGather.CountUpdate(Random.Range(12, 15));
	}

// Parse item list
	public List<Item> GetItems() {
		return lItem;
	}

// Spawn Tool based on value
	public void SpawnTool(string type) {
	// Axe
		if (type == "Wood")
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Axe").iCount > 0)
				{ Transform tAxe = Instantiate(lTools.ToArray()[0], rPlayer.transform) as Transform; tAxe.name = "Axe"; }
	// Pickaxe
		if (type == "Stone")
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Pickaxe").iCount > 0)
				{ Transform tAxe = Instantiate(lTools.ToArray()[2], rPlayer.transform) as Transform; tAxe.name = "Pickaxe"; }
	// Hoe
		if (type == "Cotton")
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				{ Transform tAxe = Instantiate(lTools.ToArray()[1], rPlayer.transform) as Transform; tAxe.name = "Hoe"; }
	// Hoe
		if (type == "Berry")
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				{ Transform tAxe = Instantiate(lTools.ToArray()[1], rPlayer.transform) as Transform; tAxe.name = "Hoe"; }
	}

// Despawn Tool
	public void DespawnTool() {
		foreach (Transform child in rPlayer.transform)
			GameObject.DestroyImmediate(child.gameObject);
	}

// Get Speed Modifier based off level
	public float SpeedModify(string type) {
		float fModifier = 0.07f;
		float fTimes = 0.0f;
	// Axe
		if (type == "Wood") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Axe").iCount > 0)
				fTimes += 0.5f;
		// Additional Levels
			fTimes += rSkillSystem.GetLevel(1) * 0.5f;
		}
	// Pickaxe
		if (type == "Stone") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Pickaxe").iCount > 0)
				fTimes += 0.5f;
		// Additional Levels
			fTimes += rSkillSystem.GetLevel(2) * 0.5f;
		}
	// Hoe
		if (type == "Cotton") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				fTimes += 0.5f;
		// Additional Levels
			fTimes += rSkillSystem.GetLevel(0) * 0.5f;
		}
	// Hoe
		if (type == "Berry") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				fTimes += 0.5f;
		// Additional Levels
			fTimes += rSkillSystem.GetLevel(0) * 0.5f;
		}
		return fModifier * fTimes;
	}

// Get Item Modifier based off level
	public int ItemModify(string type) {
		int iModifier = 0;
	// Axe
		if (type == "Wood") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Axe").iCount > 0)
				iModifier += 1;
		// Additional Levels
			iModifier += rSkillSystem.GetLevel(4);
		}
	// Pickaxe
		if (type == "Stone") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Pickaxe").iCount > 0)
				iModifier += 1;
		// Additional Levels
			iModifier += rSkillSystem.GetLevel(5);
		}
	// Hoe
		if (type == "Cotton") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				iModifier += 1;
		// Additional Levels
			iModifier += rSkillSystem.GetLevel(3);
		}
	// Hoe
		if (type == "Berry") {
		// Check for Avaliability
			if (lItem.Find(i => i.sName == "Hoe").iCount > 0)
				iModifier += 1;
		// Additional Levels
			iModifier += rSkillSystem.GetLevel(3);
		}

		return iModifier;
	}
}
