using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter2 : MonoBehaviour
{
        public GameManager gameMan;
        public int[] solution;
        public string[] clues = {"","","","","","","","","","","","","","","","","",""};

        public string[] o1 = {"Poster","Frutas","Mapa"};
        public string[] o2 = {"Apagado","Encendido con volumen alto","Encendido con volumen alto"};
        public string[] o3 = {"Cerrada","Entreabierta","Abierta"};
        public string[] o4 = {"Apagado","Encendido pero fijo","Encendido pero en movimiento"};
        public string[] o5 = {"Frutas encima","Florero encima","Nada encima"};
        public string[] o6 = {"Reclinable","Mecedora","Sofa"};
        public string[] o7 = {"Tocadisco","Radio moderna","Radio antigua"};
        public string[] o8 = {"Gato","Perro","Loro"};

        public string[] objects;

        //public string[] objects = {o1,o2};
     //   public string[][] objects = {o1,o2,o3,o4,o5,o6,o7,o8};
    
    void Start(){
          Debug.Log("clueHinter start2");
          GenerateClues(3,9,8);
          solution = gameMan.combination;
          //Debug.Log(GetClueTemplate(0,"a","b","",""));
    }

    public string[] GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed)
    {
        Debug.Log("generating clues");
        int sum = clueCountPositive + clueCountNegative + clueCountMixed;

        for (int i = 0; i < sum; i++)
        {
            if(clueCountPositive > 0){

                clues[i] = GetClueTemplate(0,"a","b","","");
                Debug.Log(clues[i]);
                clueCountPositive--;
            }

            if(clueCountPositive == 0 && clueCountNegative > 0){
                clues[i] = GetClueTemplate(1,"c","d","","");
                Debug.Log(clues[i]);
                clueCountNegative--;
            }

             if(clueCountNegative == 0 && clueCountMixed > 0){
                clues[i] = GetClueTemplate(2,"e","f","","");
                Debug.Log(clues[i]);
                clueCountMixed--;
            }
        }
       /* for (int i = 0; i < clueCountPositive; i++)
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
        }*/

        clues = reshuffle(clues);

        return clues;
        /*
         * La idea sería conseguir clueCount cantidad de GetClueTemplate para poder armar las pistas iniciales y poder
         * darselas a nuestro usuario.
         */
    }

   /* public string[] replaceClues(){
        int[] alreadyUsed = {};
        for (int i = 0; i < 8; i++)
        {
            return [];
        }
    }*/

        public string GetClueTemplate(int number, string obj, string stat, string obj2, string stat2)
    {
        /*
         * Esta es una idea de como generar los tipos de pistas.
         */
         string ret = "";
        switch (number)
        {
            case 0:
               ret = "Me gusta cuando "+obj+" esta como "+stat+"";
                return ret;
            case 1:
                ret =  "No me gusta "+obj+" cuando esta como "+stat+"";
                return ret;
            case 2:
                ret =  "Si "+obj+" esta "+stat+" entonces tampoco me gusta que "+obj2+" este "+stat2+"";
                return ret;
            default:
                return "An error has ocurred. Rip Game.";
        }
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
}