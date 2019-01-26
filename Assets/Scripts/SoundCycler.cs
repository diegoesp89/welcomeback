using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SoundCycler : MonoBehaviour {
	public AudioClip[] sounds;
	public GameManager gameManager;
	public int id;

	private int currentSound;
	private AudioSource source;
	
	void Start (){
		if(gameManager==null) {
			gameManager = FindObjectOfType<GameManager>();
		}

		source = GetComponent<AudioSource>();
		//Make initial image random.
		currentSound = Random.Range(0,2);
		source.clip = sounds[currentSound];
		source.Play();
		gameManager.ChangeObjectState(id, currentSound+1);
	}


	public void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			CycleSound();
		}
	}

	private void CycleSound() { 
		currentSound++;
		if (currentSound >= sounds.Length) {
			currentSound = 0;
		}

		GetComponent<AudioSource>().clip = sounds[currentSound];
		
		source.Play();
	}
}
