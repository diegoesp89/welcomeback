using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCycler : MonoBehaviour {
	public Sprite[] images;

	private int currentImage;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
