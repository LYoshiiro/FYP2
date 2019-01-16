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
		}
	}

}
