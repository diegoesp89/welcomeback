using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter : MonoBehaviour
{
    public string[] clues = {"","","","","","","","","","","","","","","","","",""};
    /**
     * Clue Format
     * type: 0->Positive, 1->Negative, 2->XOR
     * info: (Object, Variant)
     * info2: (Object, Variant) Used if type == 2
     * Note: 0 <= Object < Number of Objects
     *       0 <= Variant < Number of Variants
     */

    public struct Clue
    {
        public int type;
        public int object1;
        public int variant1;
        public int object2;
        public int variant2;
    }

    
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public void Start(){
        Debug.Log("clueHinter start");
       /* if (gameManRef == null) {
            gameManRef = FindObjectOfType<GameManager>();
        }*/

       // GenerateClues(2,8,8);
    }
    public string[] GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed)
    {
        Debug.Log("generating clues");
        int count = 0;
        for (int i = 0; i < clueCountPositive; i++)
        {
            clues[i] = GetClueTemplate(0);
            Debug.Log(clues[i]);
            count++;
        }
        for (int i = 0; i < clueCountNegative; i++)
        {
            clues[i + count] = GetClueTemplate(1);
             Debug.Log(clues[i]);
            count++;
        }
        for (int i = 0; i < clueCountMixed; i++)
        {
            clues[i + count] = GetClueTemplate(2);
             Debug.Log(clues[i]);
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

    public string[] gimmeClues(int person, int cluesPerPerson){
        //int person es el id de la persona, debe empezar por 0 y si son 4 personas llega hasta 3
        int cluesCount = clues.Length;
        string[] cluesResponse = new string[cluesPerPerson];
        int index = 0;

        int rangeMax = (person + 1) * cluesPerPerson;
        int rangeMin = (person - 1) * cluesPerPerson;
        rangeMin = (rangeMin < 0) ? 0 : rangeMin;
        rangeMax = (rangeMax > cluesCount) ? cluesCount : rangeMax;

        for (int i = rangeMin; i < rangeMax; i++)
        {
            cluesResponse[index] = clues[index];
            index++;
        }

        return cluesResponse;
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
     * Gets List of Clues and checks if it's enough information to solve the puzzle without contradictions
     */
    private bool CheckValidClues(List<Clue> Clues)
    {
        int Objects = 8, Variants = 3;

        int[,] SolArray = new int[Objects, Variants]; //Objects, Variants
        //Fill SolArray
        for (int i = 0; i < Objects; i++) for (int j = 0; j < Variants; j++)
                SolArray[i, j] = -1;

        //First Pass /Preprocessing
        foreach (var C in Clues)
        {
            switch (C.type)
            {
                case 0:
                    for (int i = 0; i < Variants; i++)
                    {
                        if (SolArray[C.object1, i] > 1)
                        {
                            int o, v;
                            v = (SolArray[C.object1, i] - 2) % Variants;
                            o = (SolArray[C.object1, i] - 2) / Variants;
                        }
                        SolArray[C.object1, i] = (i == C.variant1) ? 1 : 0;
                    }
                    break;
                case 1:
                    SolArray[C.object1, C.variant1] = 0;
                    break;
                case 2:
                    if (SolArray[C.object1, C.variant1] == 1 && SolArray[C.object1, C.variant2] == 1)
                    {
                        Debug.Log("Contradiction with Clue Type 2");
                        return false;
                    }
                    if (SolArray[C.object1, C.variant1] == 1)
                    {
                        SolArray[C.object1, C.variant2] = 0;
                    }
                    else if (SolArray[C.object1, C.variant2] == 1)
                    {
                        SolArray[C.object1, C.variant1] = 0;
                    }
                    else
                    {
                        SolArray[C.object1, C.variant1] = C.variant2 + C.object1 * Variants + 2;
                        SolArray[C.object1, C.variant2] = C.variant1 + C.object1 * Variants + 2;
                    }
                    break;
                default:
                    Debug.Log("Error in Clue Format");
                    break;
            }
        }

        //Processing
        bool change = true;
        int count = 0;
        while (change)
        {
            count++;
            change = false;
            //Check if there's enough info for each Object
            for (int i = 0; i < Objects; i++)
            {
                //Count Negatives (0) and Positives (1)
                int neg = 0;
                int pos = 0;
                for (int j = 0; j < Variants; j++)
                {
                    neg += (SolArray[i, j] == 0) ? 1 : 0;
                    pos += (SolArray[i, j] == 1) ? 1 : 0;
                }

                if (pos == 1)
                    continue;
                if (pos > 1)
                {
                    Debug.Log("Multiple Positives per Object");
                    return false;
                }
                if (neg == Variants)
                {
                    Debug.Log("All variants are Negative");
                    return false;
                }
                if (neg == Variants - 1)
                {
                    change = true;
                    for (int j = 0; j < Variants; j++) if (SolArray[i, j] != 0)
                        {
                            int o, v;
                            v = (SolArray[i, j] - 2) % Variants;
                            o = (SolArray[i, j] - 2) / Variants;

                            if (SolArray[o, v] == 1)
                            {
                                Debug.Log("Contradiction with Clue type 2");
                                return false;
                            }
                            SolArray[i, j] = 1; //This has to be true (all others variants are false)
                            SolArray[o, v] = 0; //This can't be true
                            break;
                        }
                }
            }
        }
        //Re-do checks if less strict conditions are needed
        int solved = 0;
        for (int i = 0; i < Objects; i++) for (int j = 0; j < Variants; j++) solved += (SolArray[i, j] == 1) ? 1 : 0;

        if (solved < Objects)
        {
            Debug.Log("Clues aren't enough");
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
    /**
     * Shows individual clue, from a person.
     */
    public string GetClue(int index, string obj, string state) {
        return clues[index].Replace("%s%", state).Replace("%o%", obj);
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
