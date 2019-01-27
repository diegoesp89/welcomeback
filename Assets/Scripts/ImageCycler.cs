using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImageCycler : MonoBehaviour {
	public Sprite[] images;
	public int id;
	public GameManager gameManager;
	
	private int currentImage;

	void LateStart (){
		if(gameManager==null) {
			gameManager = FindObjectOfType<GameManager>();
		}
		//Make initial image random.
		currentImage = Random.Range(0,2);
		GetComponent<SpriteRenderer>().sprite = images[currentImage];
		gameManager.ChangeObjectState(id, currentImage+1);
		StartCoroutine(LateStart(2f));

	}
	IEnumerator LateStart(float waitTime){
		yield return new WaitForSeconds(waitTime);
		//Your Function You Want to Call
		if (gameObject.GetComponent<SoundCycler>() != null) {
			GetComponent<SoundCycler>().ChangeSound(currentImage);
			Debug.Log(this.name);
		}
		else {
			Debug.Log(this.name);
		}
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
		if (GetComponent<SoundCycler>() != null) {
			GetComponent<SoundCycler>().ChangeSound(currentImage);
		}

		GetComponent<SpriteRenderer>().sprite = images[currentImage];
		gameManager.ChangeObjectState(id, currentImage + 1); // Necesitamos el +1 porque en el game manager las combinaciones estan puestas del 1 al 3.
	}
}