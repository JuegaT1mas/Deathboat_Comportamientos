using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    [Header("References")]
    private GameObject playerRef; //La referencia al jugador
    private GameObject enemyRef; //la referencia al enemigo
    //private FirstPersonController firstPersonController; //La referencia al firstPersonController
    public AudioSource discoveredSound;

    [Header("Escape")]
    //El gameObject de la salida
    public GameObject escape;
    //El gameobject de la grua
    public GameObject grua;
    //El collider que bloquea la salida
    public GameObject bloqueo;

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
    //Si est� activado alg�n men� que pausa el juego
    public bool isPaused = false;
    //El texto de los puzzles
    public TMP_Text puzzleUI;
    //El texto de la confimaci�n visual
    public GameObject visualUI;
    //El canvas de todos los elementos a la vez de la UI ingame
    public GameObject canvasUI_Ingame;
    //El canvas del GameOver
    public GameObject canvasGameOver;
    //El canvas del GameOverVictory
    public GameObject canvasGameOverVictory;
    //El canvas de los controles de m�vil
    public GameObject canvasMobileUI;
    //El canvas del menu de pausa
    public GameObject canvasPauseMenu;
    //El ojo abierto
    public Texture openEye;
    //El ojo cerrado
    public Texture closedEye;
    //Booleano para controlar el cambio del ojo
    private bool changedEye = false;
    //Los corazones que indican la vida
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public Texture brokenheart;


    [Header("Ajustes")]
    //El script de los ajustes, para checkearlos al principio
    public SettingsMenu settingsMenu;
    //Bool para checkear la primera vez
    private bool checkSettings = false;

    private void Start()
    {

        //Deshacer los cambios en caso de que terminemos una partida y le demos a jugar otra vez
        Time.timeScale = 1; //Reanudamos por si acaso el timeScale
        DeactivateMouse();


        playerRef = GameObject.FindGameObjectWithTag("Player"); //Encontrar la referencia al jugador al empezar
        enemyRef = GameObject.FindGameObjectWithTag("Enemy"); //Encontrar la referencia al enemigo al empezar
        GeneratePuzzles(); //Genera en el mapa los puzzles
        UpdatePuzzleUI(); //Actualiza el UI de los puzzles
        UpdateLivesUI(); //Actualiza el UI de las vidas

        settingsMenu.InitialValues();

    }

    private void Update()
    {
        UpdateVisualConfirmation();
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

        puzzlesCompleted = new bool[finalSize]; //Creamos el array de booleanos del tama�o de los puzzles elegidos

        for (int i = 0; i < finalSize; i++) //Recorremos el array de puzzles
        {
            int random = Random.Range(0, puzzlesCoords.Count); //Escogemos un indice aleatorio
            finalPuzzles[i].transform.position = puzzlesCoords[random].position; //Le ponemos la posicion aleatoria
            finalPuzzles[i].gameObject.SetActive(true); //Activamos el puzzle
            puzzlesCoords.RemoveAt(random); //Lo borramos de las opciones para que no se repita
        }
    }

    public void PuzzleCompleted()//No se si es mas efectivo dividir esto en 2 metodos o no (de momento lo divido)
    {
        enemyRef.GetComponent<EnemyManager>().puzzleCompletado = true; //Avisamos al bicho que se ha completado un puzzle

        bool terminado = true;
        for (int i = 0; i < finalPuzzles.Length; i++)
        {
            PuzzlePadre p = finalPuzzles[i].GetComponent<InteractiveObject>().puzzle.GetComponent<PuzzlePadre>();
            //Que los puzzles tengan herencia de una clase que indique por lo menos si el puzzle esta completado o no
            if(p.resuelto == false){ //Si no hemos terminado todos los puzzles no ha acabado el juego
                terminado = false;
            }else{
                finalPuzzles[i].SetActive(false);
            };
            puzzlesCompleted[i] = p.resuelto; //Actualizamos el array
        }
        if (!terminado) //Si no se han terminado todos los puzzles
        {
            UpdatePuzzleUI(); //Actualiza la UI de los que te quedan
        }
        else
        {
            ActivateEscape(); //Si has terminado los puzzles activa la salida
        }

        //firstPersonController.OnLeavePuzzle();
    }

    public void GameOver() //Funci�n que pasa cuando pierdes
    {
        isPaused = true; //Se indica que se para un men�

        ActivateMouse();
        //Ponemos el timeScale al 0 para que las cosas que dependan del tiempo no se actualicen
        Time.timeScale = 0;
        canvasGameOver.SetActive(true); //Activamos el canvas del GameOver
        playerRef.GetComponent<FirstPersonController>().DeactivatePlayer();
    }

    public void GameOverVictory() //Funci�n que pasa cuando ganas
    {
        isPaused = true; //Se indica que se para un men�

        ActivateMouse();
        //Ponemos el timeScale al 0 para que las cosas que dependan del tiempo no se actualicen
        Time.timeScale = 0;
        canvasGameOverVictory.SetActive(true);//Activamos el canvas del GameOverVictory
        playerRef.GetComponent<FirstPersonController>().DeactivatePlayer();
    }

    public void ActivateEscape() //Activar la salida
    {
        bloqueo.GetComponent<BoxCollider>().enabled = false;
        escape.gameObject.SetActive(true); //Activamos la salida
        grua.GetComponent<MeshCollider>().enabled = true;
        puzzleUI.text = "Escape boat unlocked\n�Find it!"; //Cambiamos el texto en pantalla
        //Cambiar m�sica
    }

    public void UpdatePuzzleUI()//Actualizar el UI de los puzzles
    {
        string text = "Completed Puzzles:\n"; //Creamos el string previamente
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
        puzzleUI.text = text; //Se le aplica el texto a la UI
    }

    public void UpdateLivesUI()//Se le actualiza el texto con la variable vidas del jugador actualizadas
    {
        enemyRef.GetComponent<EnemyManager>().Rest();
        int lives = playerRef.GetComponent<FirstPersonController>().lives;
        if (lives > 0) //Si quedan vidas se actualiza la pantalla
        {
            switch (lives)
            {
                case 2:
                    corazon3.GetComponent<RawImage>().texture = brokenheart;
                    break;
                case 1:
                    corazon2.GetComponent<RawImage>().texture = brokenheart;
                    break;
            }
        }
        else //Si no quedan vidas
        {
            GameOver(); //Se llama al final de la partida
        }
    }

    public void UpdateVisualConfirmation()
    {
        if (enemyRef.GetComponent<FieldOfView>().canSeePlayer)
        {
            if (!changedEye)
            {
                ChangeTexture(openEye);
                discoveredSound.Play();
            }
            changedEye = true;
        }
        else
        {
            if (changedEye)
            {
                ChangeTexture(closedEye);
            }
            changedEye = false;
        }
    }

    private void ChangeTexture(Texture tex)
    {
        visualUI.GetComponent<RawImage>().texture = tex;
    }

    public void CheckDevice(PlayerInput playerInput) //Comprobaci�n de los controles para activar el m�vil
    {
        if(playerInput.currentControlScheme == "Touch")
        {
            canvasMobileUI.SetActive(true);
        }
        else
        {
            canvasMobileUI.SetActive(false);
        }
    }

    public void ActivateMouse() //Activar el mouse por pantalla
    {
        //Desbloqueamos el rat�n para poder clickear
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DeactivateMouse()//Desactivar el mouse por pantalla
    {
        //Quitamos el rat�n 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ActivatePauseMenu()
    {
        isPaused = true; //Se indica que se para un men�

        Time.timeScale = 0;
        canvasUI_Ingame.SetActive(false);
        canvasMobileUI.SetActive(false);
        canvasPauseMenu.SetActive(true);
        ActivateMouse();
    }

    public void ResumeGame()
    {
        isPaused = false; //Se indica que se reanuda

        DeactivateMouse();
        playerRef.GetComponent<FirstPersonController>().OnControlsChanged();
    }

}
