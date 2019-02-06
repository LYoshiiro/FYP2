using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {
// Data Values
	[SerializeField] private int iSlide;
	[SerializeField] private List<Sprite> lSprite;
	private Image iImage;

	private void Start() {
	// Set Reference
		iImage = transform.GetComponent<Image>();
	// Set Initial Value
		iSlide = 0;
	}

// Left Button Menu
	public void LeftArrow() {
		iSlide--;
		if (iSlide < 0)
			iSlide = 4;
		iImage.sprite = lSprite.ToArray()[iSlide];
	}

// Right Button Menu
	public void RightArrow() {
		iSlide++;
		if (iSlide > 4)
			iSlide = 0;
		iImage.sprite = lSprite.ToArray()[iSlide];
	}

// Close Button Menu
	public void CloseMenu() {
	// Close the Menu
		transform.gameObject.SetActive(false);
	}
}
