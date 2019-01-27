using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	public float scrollSpeed = 0.2f;

	public float tolerance = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var mouseX = Input.mousePosition.x;
		if (mouseX < tolerance) {
			Camera.main.transform.position += Vector3.left * scrollSpeed;
		} else if (mouseX > Screen.width - tolerance) {
			Camera.main.transform.position -= Vector3.left * scrollSpeed;
		}
	}
}
