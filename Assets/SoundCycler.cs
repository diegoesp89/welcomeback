using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCycler : MonoBehaviour {
	public AudioClip[] sounds;

	private int currentSound;
	

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
	}
}
