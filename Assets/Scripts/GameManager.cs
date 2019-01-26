using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int[] combination; // Final combination.
    public List<Objeto> Objects; //Real time object variations.

    private struct Objeto
    {
        public string name;
        public List<string> variants;
        public int selected;
    }
    public void StartGame()
    {

    }

    public void Start()
    {
        Debug.Log("Start!");
        GenerateCombination(8);
    }

    public void GenerateCombination(int objectCount)
    {
        combination = new int[objectCount];
        for (int i = 0; i < objectCount; i++)
        {
            combination[i] = Random.Range(1, 3); //1, 2 or 3.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
        Debug.Log("Combination ready");
    }

    public void ChangeObjectState(int id, int state)
    {
        Objects[id].selected = state;
        if (CheckWin())
        {
            SceneManager.LoadScene(2);
        }
    }

    public bool CheckWin()
    {
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