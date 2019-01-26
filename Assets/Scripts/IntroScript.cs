using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {
	public Image img;
	public SceneManagement sceneManager;
	
	
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
			"Can you help the Rogers find the combination that will make them feel at home again?";
		yield return new WaitForSeconds(5f);
		StartCoroutine(FadeImage(false));
		yield return new WaitForSeconds(2);
		sceneManager.ChangeScene(1); //Changes the scene into the game.

	}
	
	IEnumerator FadeImage(bool fadeAway){
		// fade from opaque to transparent
		if (fadeAway)
		{
			// loop over 1 second backwards
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				// set color with i as alpha
				img.color = new Color(0, 0, 0, i);
				yield return null;
			}
		}
		// fade from transparent to opaque
		else
		{
			// loop over 1 second
			for (float i = 0; i <= 1; i += Time.deltaTime)
			{
				// set color with i as alpha
				img.color = new Color(0, 0, 0, i);
				yield return null;
			}
		}
	}

}
