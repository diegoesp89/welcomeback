
using UnityEngine;

public class GameManager : UnityEngine.MonoBehaviour {
    private int[] combination;
    
    
    public void StartGame() {
        
    }

    public void GenerateCombination(int objectCount) {
        for (int i = 0; i < objectCount; i++) {
            combination[i] = Random.Range(0, 2); //0, 1 or 2.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }
}