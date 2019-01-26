using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImageCycler : MonoBehaviour {
	public Sprite[] images;
	public int id;
	public GameManager gameManager;
	
	private int currentImage;

	void Start (){
		if(gameManager==null) {
			gameManager = FindObjectOfType<GameManager>();
		}
		//Make initial image random.
		currentImage = Random.Range(0,2);
		GetComponent<SpriteRenderer>().sprite = images[currentImage];
		gameManager.ChangeObjectState(id, currentImage+1);
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
		gameManager.ChangeObjectState(id, currentImage + 1); // Necesitamos el +1 porque en el game manager las combinaciones estan puestas del 1 al 3.
	}
}
