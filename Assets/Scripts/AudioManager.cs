using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	private AudioClip currentSong;


	public void ChangeSong(AudioClip clip) {
		StartCoroutine(FadeOut(1, clip)); //FIRST FLOAT IS THE AMOUNT IN SECONDS
	}

	public void ChangeClip(AudioClip clip) {
		currentSong = clip;
		GetComponent<AudioSource>().clip = currentSong;
		GetComponent<AudioSource>().Play();
		GetComponent<AudioSource>().volume = 1;
	}
	public IEnumerator FadeOut (float FadeTime, AudioClip nextClip) {
		var audioSource = GetComponent<AudioSource>();
		float startVolume = audioSource.volume;
 
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
			yield return null;
		}
 
		audioSource.Stop ();
		audioSource.volume = startVolume;
		
		ChangeClip(nextClip);
	}

}
