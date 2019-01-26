using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    private int[] combination; // Final combination.
    private int[] objectStates = {1,1,1,1,1,1,1,1}; //Real time object variations.

    private string[] clues;
    
    
    public void StartGame() {
       
    }

    public void Start() {
        Debug.Log("Start!");
        //GenerateCombination(8);
        //GenerateClues(2,8,8);
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
    
    

    public string[] GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed) {

        int count = 0;
        for(int i = 0; i < clueCountPositive; i++){
            clues[i] = GetClueTemplate(0);
            count++;
        }
        for(int i = 0; i < clueCountNegative; i++){
            clues[i+count] = GetClueTemplate(1);
            count++;
        }
        for(int i = 0; i < clueCountMixed; i++){
            clues[i+count] = GetClueTemplate(2);
            count++;
        }

        Debug.Log("Clues count: " + count);
        clues = reshuffle(clues);

        return clues;
        /*
         * La idea sería conseguir clueCount cantidad de GetClueTemplate para poder armar las pistas iniciales y poder
         * darselas a nuestro usuario.
         */
    }

    public string[] reshuffle(string[] clues)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < clues.Length; i++ )
        {
            string tmp = clues[i];
            int rand = Random.Range(i, clues.Length);
            clues[i] = clues[rand];
            clues[rand] = tmp;
        }

        return clues;
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
                return "I like the %o% like %s%";
            case 1:
                return "I dont like %s% %o%";
            case 2:
                return "I really dont like when %o% is %s%, and %o% is %s% at the same time";
            default:
                return "An error has ocurred. Rip Game.";
        }
    }
    
}