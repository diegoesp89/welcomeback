using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int howManyPerson;
    /**
     * Clue Format
     * type: 0->Positive, 1->Negative, 2->XOR
     * info: (Object, Variant)
     * info2: (Object, Variant) Used if type == 2
     */
    private struct Clue
    {
        public int type;
        public (int o, int v) info;
        public (int o, int v) info2;
    }
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
     * Gets List of Clues and checks if it's enough information to solve the puzzle
     */
    private bool CheckValidClues(List<Clue> Clues)
    {
        int Objects = 8, Variants = 3;

        int[,] SolArray = new int[Objects, Variants]; //Objects, Variants
        //Fill SolArray
        for (int i = 0; i < Objects; i++) for (int j = 0; j < Variants; j++)
                SolArray[i, j] = -1;

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
                    //Something went wrong
                }
                if (neg == Variants)
                {
                    //Something went wrong
                }
                if (neg == Variants - 1)
                {
                    change = true;
                    for (int j = 0; j < Variants; j++) if (SolArray[i, j] != 0)
                        {
                            int o, v;
                            o = (SolArray[i, j] - 2) % Variants;
                            v = (SolArray[i, j] - 2) / Variants;

                            if (SolArray[o, v] == 1)
                            {
                                //Something went wrong
                            }
                            SolArray[i, j] = 1; //This has to be true (all others variants are false)
                            SolArray[o, v] = 0; //This can't be true
                            break;
                        }
                }
            }
        }
        int solved = 0;
        for (int i = 0; i < Objects; i++) for (int j = 0; j < Variants; j++) solved += (SolArray[i, j] == 1) ? 1 : 0;

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
