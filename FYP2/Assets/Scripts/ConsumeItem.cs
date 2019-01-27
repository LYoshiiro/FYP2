using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeItem : MonoBehaviour {
// Reference
	[SerializeField] private Core rCore;
	[SerializeField] private Item rItem;
	[SerializeField] private PlayerInventory rPlayer;

	private void Start() {
		GetComponent<Button>().onClick.AddListener(Consume);
	}

// Set Reference
	public void SetReference(Core core, Item item, PlayerInventory player) {
		rCore = core;
		rItem = item;
		rPlayer = player;
	}

// Consume Item
	private void Consume() {
		rItem.CountUpdate(-1);
		rCore.Pnt("WH");
		rPlayer.bMenuChange = true;
	}
}
