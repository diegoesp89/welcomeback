using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    private int[] combination; // Final combination.
    private int[] objectStates = {1,1,1,1,1,1,1,1}; //Real time object variations.

    private string[] clues;
    
    
    public void StartGame() {
        
    }

    public void GenerateCombination(int objectCount) {
        for (int i = 0; i < objectCount; i++) {
            combination[i] = Random.Range(1, 3); //0, 1 or 2.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }

    public void ChangeObjectState(int id, int state) {
        objectStates[id] = state;
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
    
    

    public void GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed) {

        int count = 0;
        for(int i = 0; i < clueCountPositive; i++){
            clues[i] = GetClueTemplate(0);
            count++;
        }
        for(int i = 0; i < clueCountNegative; i++){

        }
        for(int i = 0; i < clueCountMixed; i++){

        }

        /*
         * La idea sería conseguir clueCount cantidad de GetClueTemplate para poder armar las pistas iniciales y poder
         * darselas a nuestro usuario.
         */
    }

    /**
     * Gets a number and returns the corresponding template.
     */
    public string GetClueTemplate(int number) {
        /*
         * Esta es una idea de como generar los tipos de pistas.
         */
        switch (number) {
            case 0:
                return "The %o% is %j%";
            case 1:
                return "The %o% is not %j%";
            case 2:
                return "The %0% likes the same TV Volume as the %1%";
            default:
                return "An error has ocurred. Rip Game.";
        }
    }
    
}