using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter2 : MonoBehaviour
{
        public GameManager gameMan;
        public int[] solution;
        public string[] clues = {"","","","","","","",""};
        public string[] objectsClickable = {"Cuadro","Television","Cortina","Ventilador","Mesa de Centro","Silla","Reproductor de musica","Mascota"};
        public string[] o0 = {"Poster","Frutas","Mapa"};
        public string[] o1 = {"Apagado","Encendido con volumen alto","Encendido con volumen alto"};
        public string[] o2 = {"Cerrada","Entreabierta","Abierta"};
        public string[] o3 = {"Apagado","Encendido pero fijo","Encendido pero en movimiento"};
        public string[] o4 = {"Frutas encima","Florero encima","Nada encima"};
        public string[] o5 = {"Reclinable","Mecedora","Sofa"};
        public string[] o6 = {"Tocadisco","Radio moderna","Radio antigua"};
        public string[] o7 = {"Gato","Perro","Loro"};

    

        //public string[] objects = {o1,o2};
     //   public string[][] objects = {o1,o2,o3,o4,o5,o6,o7,o8};
    
    void Start(){
          Debug.Log("clueHinter start2");
          GenerateCombination(8);
          GenerateClues(2,3,3);
          //solution = gameMan.combination;
          //Debug.Log(GetClueTemplate(0,"a","b","",""));
    }

  public void GenerateCombination(int objectCount) {
        solution = new int[objectCount];
        for (int i = 0; i < objectCount; i++) {
            solution[i] = Random.Range(0, 3); //1, 2 or 3.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }

    public int gimmeOneFalse(int number)
    {
        switch (number)
        {
            case 0:
            return 2;
            case 1:
            return 0;
            case 2:
            return 1;
            default:
            return 0;
        }
    }
    public string[] GenerateClues(int clueCountPositive, int clueCountNegative, int clueCountMixed)
    {
        Debug.Log("generating clues");
        int sum = clueCountPositive + clueCountNegative + clueCountMixed;
        int status = 0;
        string statusLabelTrue = "Status";
        string statusLabelFalse = "Status";
        for (int i = 0; i < clues.Length; i++)
        {
            status = solution[i];
            Debug.Log(status);
           switch (i)
            {
                case 0:
                    statusLabelTrue = o0[solution[0]];
                    statusLabelFalse = o0[gimmeOneFalse(solution[0])];
                    break;
                case 1:
                    statusLabelTrue = o1[solution[1]];
                    statusLabelFalse = o1[gimmeOneFalse(solution[1])];
                    break;
                case 2:
                    statusLabelTrue = o2[solution[2]];
                    statusLabelFalse = o2[gimmeOneFalse(solution[2])];
                    break;
                case 3:
                    statusLabelTrue = o3[solution[3]];
                    statusLabelFalse = o3[gimmeOneFalse(solution[3])];
                    break;
                case 4:
                    statusLabelTrue = o4[solution[4]];
                    statusLabelFalse = o4[gimmeOneFalse(solution[4])];
                    break;
                case 5:
                    statusLabelTrue = o5[solution[5]];
                    statusLabelFalse = o5[gimmeOneFalse(solution[5])];
                    break;
                case 6:
                    statusLabelTrue = o6[solution[6]];
                    statusLabelFalse = o6[gimmeOneFalse(solution[6])];
                    break;
                case 7:
                    statusLabelTrue = o7[solution[7]];
                    statusLabelFalse = o7[gimmeOneFalse(solution[7])];
                    break;
                default:
                break;
            } 
            Debug.Log("objeto:"+objectsClickable[i]);
            Debug.Log("labeltru:"+statusLabelTrue);
            Debug.Log("labelfalse"+statusLabelFalse);

            if(clueCountPositive > 0){
                clues[i] = GetClueTemplate(0,objectsClickable[i],statusLabelTrue,"","");
                Debug.Log(clues[i]);
                clueCountPositive--;
            }

            if(clueCountPositive == 0 && clueCountNegative > 0){
                clues[i] = GetClueTemplate(1,objectsClickable[i],statusLabelFalse,"","");
                Debug.Log(clues[i]);
                clueCountNegative--;
            }

             if(clueCountNegative == 0 && clueCountMixed > 0){
                clues[i] = GetClueTemplate(2,objectsClickable[i],statusLabelTrue,"","");
                Debug.Log(clues[i]);
                clueCountMixed--;
            }
        }

        //clues = reshuffle(clues);

        return clues;
        /*
         * La idea sería conseguir clueCount cantidad de GetClueTemplate para poder armar las pistas iniciales y poder
         * darselas a nuestro usuario.
         */
    }


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