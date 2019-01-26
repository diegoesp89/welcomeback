using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImageCycler : MonoBehaviour {
	public Sprite[] images;

	private int currentImage;
	
	
	
	public void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			CycleImage();
		}
	}

	private void CycleImage() {
		currentImage++;
		if (currentImage >= images.Length) {
			currentImage = 0;
		}

		GetComponent<SpriteRenderer>().sprite = images[currentImage];
	}
}
