using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIndicator : MonoBehaviour {
// Modifier
	[SerializeField] private Texture2D tCursorOriginal;
	[SerializeField] private Texture2D tCursorChanged;
	private CursorMode cMode = CursorMode.Auto;
	
	private void Start() {
	// Reset the cursor
		SetCursor(0);
	}

// Set Cursor
	public void SetCursor(int value) {
		switch (value) {
			case 0: Cursor.SetCursor(tCursorOriginal, Vector2.zero, cMode); break;
			case 1: Cursor.SetCursor(tCursorChanged, Vector2.zero, cMode); break;
		}
	}
}
