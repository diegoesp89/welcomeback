using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform>().position = new Vector3(-2000, -400);
	}
	

	public void ShowDialog(string text, float duration) {
		StartCoroutine(DialogWorker(text, duration));
	}

	private IEnumerator DialogWorker(string text, float duration) {
		var position = GetComponent<RectTransform>().localPosition;
		position.x = 67;
		position.y = -400;
		position.z = 0;
		GetComponent<RectTransform>().localPosition = position;
		GetComponentInChildren<Text>().text = text;
		yield return new WaitForSeconds(duration);
		
		GetComponent<RectTransform>().localPosition = new Vector3(-1800, -400,0);
		GetComponentInChildren<Text>().text = "";
	}
}
