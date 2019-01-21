using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
// Class Reference
	[SerializeField] private Core rCore;
	[SerializeField] private ItemManager rItemManager;

// List Reference
	[SerializeField] private List<Text> lText;
	[SerializeField] private List<Item> lItem;
	[SerializeField] private List<Transform> lTransform;

	private void FixedUpdate() {
    // Check if game is paused
        if (rCore.bPause != true) {
		// Parse Data
			lItem = rItemManager.GetItems();

		// FPS
			lText.ToArray()[0].GetComponent<Text>().text = "FPS: " + 1 / Time.deltaTime;

	// Items
		// Wood
			lText.ToArray()[1].GetComponent<Text>().text = "Wood: " + lItem.Find(i => i.sName == "Wood").iCount;

		// Stone
			lText.ToArray()[2].GetComponent<Text>().text = "Stone: " + lItem.Find(i => i.sName == "Stone").iCount;
		
		// Berry
			lText.ToArray()[3].GetComponent<Text>().text = "Berry: " + lItem.Find(i => i.sName == "Berry").iCount;
		
		// Cotton
			lText.ToArray()[4].GetComponent<Text>().text = "Cotton: " + lItem.Find(i => i.sName == "Cotton").iCount;

		// Raft
			lText.ToArray()[5].GetComponent<Text>().text = "Raft: " + lItem.Find(i => i.sName == "Raft").iCount;
	
	// Clothes
		// Hat
			lText.ToArray()[6].GetComponent<Text>().text = "Cotton Hat: " + lItem.Find(i => i.sName == "Cotton Hat").iCount;
		// Shirt
			lText.ToArray()[7].GetComponent<Text>().text = "Cotton Shirt: " + lItem.Find(i => i.sName == "Cotton Shirt").iCount;
		// Pant
			lText.ToArray()[8].GetComponent<Text>().text = "Cotton PAnt: " + lItem.Find(i => i.sName == "Cotton Pant").iCount;
			}

	// Update Pop-up
		lTransform.ToArray()[0].gameObject.SetActive(rCore.bWin);
	// Check if game is won
		if (rCore.bWin != true)
			lTransform.ToArray()[1].gameObject.SetActive(rCore.bPause);
	}
}
