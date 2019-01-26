using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private string[] clues;
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

    public void ShowClues() {
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
    private void OnMouseExit() {
        this.transform.localScale = new Vector3(1f, 1f);
    }
}
