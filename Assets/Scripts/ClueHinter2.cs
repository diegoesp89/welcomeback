using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClueHinter2 : MonoBehaviour
{
        public GameManager gameMan;
        public int[] solution;
        //public string[] clues = {"","","","","","","",""};
        public string[] clues = {};
        public string[] objectsClickable = {"cuando el cuadro","cuando la television","cuando las cortinas","cuando el ventilador","cuando la mesa de centro","cuando la silla","que el reproductor de musica","cuando mi mascota"};
        public string[] o0 = {"es un poster","es una pintura con frutas","es un mapa"};
        public string[] o1 = {"esta apagada", "esta encendida con volumen alto","esta encendida con volumen bajo"};
        public string[] o2 = {"estan cerradas","estan entreabiertas","estan abiertas"};
        public string[] o3 = {"esta apagado","encendido pero fijo","encendido pero en movimiento"};
        public string[] o4 = {"tiene frutas encima","tiene un florero encima","no tiene nada encima"};
        public string[] o5 = {"es reclinable","es una mecedora","es un sofa"};
        public string[] o6 = {"sea un tocadisco","sea una radio moderna","sea una radio antigua"};
        public string[] o7 = {"es un gato","es un perro","es un loro"};

        public int[] objetosIndex = {0,1,2,3,4,5,6,7};

    

        //public string[] objects = {o1,o2};
     //   public string[][] objects = {o1,o2,o3,o4,o5,o6,o7,o8};
    
    void Start(){
          Debug.Log("clueHinter start2");
          objetosIndex = reshuffleInt(objetosIndex);
          GenerateCombination(8);
          GenerateClues(2,4,2);
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
         string statusLabelTrue2 = "Status";
        string statusLabelFalse2 = "Status";

        int index = 0;
        foreach (var item in objetosIndex)
        {
            
        
            //for (int i = 0; i < sum; i++)
            //{
            bool ocupado = false;
            statusLabelTrue = "";
            statusLabelFalse = "";
            statusLabelTrue2 = "";
            statusLabelFalse2 = "";
            status = solution[item];
            Debug.Log(status);
           switch (item)
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
            Debug.Log("objeto:"+objectsClickable[item]);
            Debug.Log("labeltru:"+statusLabelTrue);
            Debug.Log("labelfalse"+statusLabelFalse);

            if(clueCountPositive >= 0){
                clues[item] = GetClueTemplate(0,objectsClickable[item],statusLabelTrue,"","");
                Debug.Log(clues[item]);
                clueCountPositive--;
            }

            if(clueCountPositive < 0 && clueCountNegative >= 0){
                clues[item] = GetClueTemplate(1,objectsClickable[item],statusLabelFalse,"","");
                Debug.Log(clues[item]);
                clueCountNegative--;
            }

             if(clueCountNegative < 0 && clueCountMixed >= 0){
                int itemOther = item-1;
                if(itemOther <= 0){
                   itemOther = itemOther+2;
                }

                if(item == itemOther){
                    itemOther++;
                }

                if(itemOther == 7){
                    itemOther = 2;
                }
                            switch (itemOther)
                        {
                            case 0:
                                statusLabelTrue2 = o0[solution[0]];
                                statusLabelFalse2 = o0[gimmeOneFalse(solution[0])];
                                break;
                            case 1:
                                statusLabelTrue2 = o1[solution[1]];
                                statusLabelFalse2 = o1[gimmeOneFalse(solution[1])];
                                break;
                            case 2:
                                statusLabelTrue2 = o2[solution[2]];
                                statusLabelFalse2 = o2[gimmeOneFalse(solution[2])];
                                break;
                            case 3:
                                statusLabelTrue2 = o3[solution[3]];
                                statusLabelFalse2 = o3[gimmeOneFalse(solution[3])];
                                break;
                            case 4:
                                statusLabelTrue2 = o4[solution[4]];
                                statusLabelFalse2 = o4[gimmeOneFalse(solution[4])];
                                break;
                            case 5:
                                statusLabelTrue2 = o5[solution[5]];
                                statusLabelFalse2 = o5[gimmeOneFalse(solution[5])];
                                break;
                            case 6:
                                statusLabelTrue2 = o6[solution[6]];
                                statusLabelFalse2 = o6[gimmeOneFalse(solution[6])];
                                break;
                            case 7:
                                statusLabelTrue2 = o7[solution[7]];
                                statusLabelFalse2 = o7[gimmeOneFalse(solution[7])];
                                break;
                            default:
                            break;
                        } 

                //o7[gimmeOneFalse(solution[itemOther]
                clues[item] = GetClueTemplate(2,objectsClickable[item],statusLabelFalse,objectsClickable[itemOther],statusLabelFalse2);
                Debug.Log(clues[item]);
                clueCountMixed--;
            }
        //}
        }

        clues = reshuffle(clues);

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
               ret = "Me gusta cuando "+obj+" "+stat+"";
                return ret;
            case 1:
                ret =  "No me gusta "+obj+" "+stat+"";
                return ret;
            case 2:
                ret =  "nunca van juntos '"+obj+" "+stat+"' y '"+obj2+" "+stat2+"'";
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
         public int[] reshuffleInt(int[] obj)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < obj.Length; i++)
        {
            int tmp = obj[i];
            int rand = Random.Range(i, obj.Length);
            obj[i] = obj[rand];
            obj[rand] = tmp;
        }

        return obj;
    }
}