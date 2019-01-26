using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public int[] combination; // Final combination.
    public int[] objectStates = {1,1,1,1,1,1,1,1}; //Real time object variations.

    
    public void StartGame() {
       
    }

    public void Start() {
        Debug.Log("Start!");
    }

    public void GenerateCombination(int objectCount) {
        combination = new int[objectCount];
        for (int i = 0; i < objectCount; i++) {
            combination[i] = Random.Range(1, 3); //1, 2 or 3.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }

    public void ChangeObjectState(int id, int state) {
        objectStates[id] = state;
        if (CheckWin()) {
            SceneManager.LoadScene(2);
        }
    }

    public bool CheckWin() {
        bool win = true;
        for (int j = 0; j < combination.Length; j++) {
            if (combination[j] != objectStates[j]) {
                win = false;
                break;
            }
        }
        //  Returns if all object variations are equal to the final combination.
        return win;
    }
    
}