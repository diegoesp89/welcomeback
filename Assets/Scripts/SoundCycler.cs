using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SoundCycler : MonoBehaviour {
	public AudioClip[] sounds;
	public GameManager gameManager;
	public AudioManager audioMan;
	
	public int id;

	private int currentSound;
	
	void Start (){
		if(gameManager==null) {
			gameManager = FindObjectOfType<GameManager>();
		}

		if (audioMan == null) {
			audioMan = FindObjectOfType<AudioManager>();
		}
		//Make initial image random.
		currentSound = Random.Range(0,2);
		audioMan.ChangeSong(sounds[currentSound]);
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

		audioMan.ChangeSong(sounds[currentSound]);		
	}
}
