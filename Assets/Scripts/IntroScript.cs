using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {
	
	// Use this for initialization
	void Start() {
		StartCoroutine(StartSequence());
	}

	private IEnumerator StartSequence() {
		Debug.Log("Initializing...");
		yield return new WaitForSeconds(6);
		GetComponent<Text>().text =
			"After some years they started to become more and more apart from each other, " +
			"living in little tolerance of their preferences. Losing track of what a family is and " +
			"turning their home into yet another house.";
		yield return new WaitForSeconds(10f);
		GetComponent<Text>().text =
			"After some years they started to become more and more apart from each other, " +
			"living in little tolerance of their preferences. Losing track of what a family is and " +
			"turning their home into yet another house.";
		GetComponent<Text>().text =
			"Can you help the Rogers find the combination that will make them feel at home again?";

	}
}
