using Random = UnityEngine.Random;

public class GameManager : UnityEngine.MonoBehaviour {
    private int[] combination;
    private string[] clues;
    
    
    public void StartGame() {
        
    }

    public void GenerateCombination(int objectCount) {
        for (int i = 0; i < objectCount; i++) {
            combination[i] = Random.Range(0, 2); //0, 1 or 2.
        }
        //GENERATES AN ARRAY OF N ELEMENTS, EACH INDEX IS AN ID OF AN OBJECT. THE NUMBERS INSIDE CORRESPOND TO THE VARIATIONS.
    }

    public void GenerateClues(int clueCount) {
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
                return "The %0% likes the same TV Volume as the %1%";
            default:
                return "An error has ocurred. Rip Game.";
        }
    }
}