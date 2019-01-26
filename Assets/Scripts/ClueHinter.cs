using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string[] clues;
    public string[] GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed)
    {

        int count = 0;
        for (int i = 0; i < clueCountPositive; i++)
        {
            clues[i] = GetClueTemplate(0);
            count++;
        }
        for (int i = 0; i < clueCountNegative; i++)
        {
            clues[i + count] = GetClueTemplate(1);
            count++;
        }
        for (int i = 0; i < clueCountMixed; i++)
        {
            clues[i + count] = GetClueTemplate(2);
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
        for (int i = 0; i < clues.Length; i++)
        {
            string tmp = clues[i];
            int rand = Random.Range(i, clues.Length);
            clues[i] = clues[rand];
            clues[rand] = tmp;
        }

        return clues;
    }

    /** 
     * Gets Clues in a non-String Format and checks if it's enough information to solve the puzzle
     */
    private bool CheckValidClues()
    {
        int Objects = 8, Variants = 3;

        int[,] SolArray = new int[Objects, Variants]; //Objects, Variants
        //Fill SolArray
        for (size_t i = 0; i < Objects; i++) for (size_t j = 0; j < Variants; j++)
                SolArray[i][j] = -1;

        //First Pass /Preprocessing
        //Heavily Depends on Format of Clues

        //Processing
        bool change = true;
        int count = 0;
        while (change)
        {
            count++;
            change = false;
            //Check if there's enough info for each Object
            for (size_t i = 0; i < Objects; i++)
            {
                //Count Negatives (0) and Positives (1)
                int neg = 0;
                int pos = 0;
                for (size_t j = 0; j < Variants; j++)
                {
                    neg += (SolArray[i][j] == 0);
                    pos += (SolArray[i][j] == 1);
                }

                if (pos == 1)
                    continue;
                if (pos > 1)
                {
                    //Something went wrong
                }
                if (neg == Variants)
                {
                    //Something went wrong
                }
                if (neg == Variants - 1)
                {
                    change = true;
                    for (size_t j = 0; j < Variants; j++) if (SolArray[i][j] != 0)
                        {
                            int o, v;
                            //Get o and v (depends on preprocessing)

                            if (SolArray[o][v] == 1)
                            {
                                //Something went wrong
                            }
                            SolArray[i][j] = 1; //This has to be true (all others variants are false)
                            SolArray[o][v] = 0; //This can't be true
                            break;
                        }
                }
            }
        }
        int solved = 0;
        for (size_t i = 0; i < Objects; i++) for (size_t j = 0; j < Variants; j++) solved += (SolArray[i][j] == 1);

        if (solved < Objects)
        {
            //Unsolvable
            return false;
        }
        return true;
    }

    /**
     * Gets a number and returns the corresponding template.
     */
    public string GetClueTemplate(int number)
    {
        /*
         * Esta es una idea de como generar los tipos de pistas.
         */
        switch (number)
        {
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

    public void ShowClues()
    {
        //TODO SHOW BOX FOR HINTS
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("woo");
        this.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("woo");
        this.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
    }
    private void OnMouseExit()
    {
        this.transform.localScale = new Vector3(1f, 1f);
    }
}
