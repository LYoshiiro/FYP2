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
		ResetCursor();
	}

// Set new Cursor
	public void SetCursor() {
		Cursor.SetCursor(tCursorChanged, Vector2.zero, cMode);
	}

// Reset Cursor
	public void ResetCursor() {
		Cursor.SetCursor(tCursorOriginal, Vector2.zero, cMode);
	}
}
