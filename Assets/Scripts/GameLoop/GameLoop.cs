using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class GameLoop : MonoBehaviour
{

    private GameObject playerRef; //La referencia al jugador

    [Header("Puzzles")]
    //Las referencias a los puzzles
    public List<GameObject> puzzles;
    //Los comprobante de que se hayan completado los puzzles
    public bool[] puzzlesCompleted;
    //Las coordenadas donde pueden estar los puzzles
    public List<Transform> puzzlesCoords;
    //Los puzzles con los que nos vamos a quedar
    private GameObject[] finalPuzzles;
    int numPuzzles = 4;

    [Header("UI")]
    //El texto de las vidas
    public TMP_Text lifeUI;
    //El texto de los puzzles
    public TMP_Text puzzleUI;


    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player"); //Encontrar la referencia al jugador al empezar
        GeneratePuzzles(); //Genera en el mapa los puzzles
        UpdatePuzzleUI(); //Actualiza el UI de los puzzles
        UpdateLivesUI(); //Actualiza el UI de las vidas
    }

    private void Update()
    {
        
    }

    public void GeneratePuzzles()
    {
        //Comprobamos si hay suficientes puzzles para llegar al numero de puzzles
        int finalSize = puzzles.Count >= numPuzzles ? numPuzzles : puzzles.Count; 

        finalPuzzles = new GameObject[finalSize]; //Creamos el array de los puzzles que van a ser elegidos
        for (int i = 0; i < finalSize; i++)
        {
            int random = Random.Range(0, puzzles.Count); //Escogemos un indice aleatorio
            finalPuzzles[i] = puzzles[random]; //Escogemos ese puzzle 
            puzzles.RemoveAt(random); //Eliminamos ese puzzle para que no sea escogido de nuevo
        }

        puzzlesCompleted = new bool[finalSize]; //Creamos el array de booleanos del tamaño de los puzzles elegidos

        for (int i = 0; i < finalSize; i++) //Recorremos el array de puzzles
        {
            int random = Random.Range(0, puzzlesCoords.Count); //Escogemos un indice aleatorio
            finalPuzzles[i].transform.position = puzzlesCoords[random].position; //Le ponemos la posicion aleatoria
            finalPuzzles[i].gameObject.SetActive(true); //Activamos el puzzle
            puzzlesCoords.RemoveAt(random); //Lo borramos de las opciones para que no se repita
        }
    }


    public void PuzzleCompleted()
    {

    }

    public void GameOver()
    {

    }

    public void UpdatePuzzleUI()
    {
        //Por terminar
        string text = "Puzzles completados:\n"; //Creamos el string previamente
        for (int i = 0; i < finalPuzzles.Length; i++)
        {
            if(puzzlesCompleted[i] == true) //Dependiendo de si el puzzle se ha completado o no 
            {
                text += "Puzzle " + (i + 1) + ": " + "Yes\n"; //Se rellena con si
            }
            else
            {
                text += "Puzzle " + (i + 1) + ": " + "No\n";// o con no
            }
            
        }
        lifeUI.text = text; //Se le aplica el texto a la UI
    }

    public void UpdateLivesUI()//Se le actualiza el texto con la variable vidas del jugador actualizadas
    {
        lifeUI.text = string.Format("Vidas restantes: {0}", playerRef.GetComponent<FirstPersonController>().lives);
    }

}
