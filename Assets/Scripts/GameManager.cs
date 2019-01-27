using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public struct Objeto {
    public string name;
    public List<string> variants;
    public int selected;
}
public class GameManager : MonoBehaviour {
    public int[] combination; // Final combination.
    public List<Objeto> Objects; //Real time object variations.

    public void StartGame()
    {

    }

    public void awake() {
        Debug.Log("Start!");
        GenerateCombination(8);
    }

    public void GenerateCombination(int objectCount) {
        combination = new int[objectCount];
        for (int i = 0; i < objectCount; i++) {
            combination[i] = Random.Range(1, 3); //1, 2 or 3.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }

    public void ChangeObjectState(int id, int state) {
        var objeto = Objects[id];
        objeto.selected = state;
        Objects[id] = objeto;
        
        if (CheckWin())
        {
            SceneManager.LoadScene(2);
        }
    }

    public int[] getCombination(){
        return combination;
    }

    public bool CheckWin() {
        bool win = true;
        for (int j = 0; j < combination.Length; j++)
        {
            if (combination[j] != Objects[j].selected)
            {
                win = false;
                break;
            }
        }
        //  Returns if all object variations are equal to the final combination.
        return win;
    }
    
}