using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);	
	}

	public void ChangeScene(int id) {
		SceneManager.LoadScene(id);
	}
}
