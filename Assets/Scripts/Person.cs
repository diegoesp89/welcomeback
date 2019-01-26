using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
public ClueHinter clueRef;
	// Use this for initialization
	void Start () {
		Debug.Log(clueRef.gimmeClues(0, 3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
