using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject puzzle; 
    public GameObject[] listaCamaras;

    public GameLoop gameLoop; //La referencia del gameLoop

    public bool isEscape; //Si el objeto es salida o no

   public void ActivarObjeto()//metodo para crear el puzzle por primera vez
   {
        puzzle.GetComponent<PuzzlePadre>().IniciarPuzzle();
        listaCamaras[0].gameObject.SetActive(false);
        listaCamaras[1].gameObject.SetActive(true);
   }

    public void Victory()
    {
        gameLoop.GameOverVictory();
    }

    public void Decide()//metodo para diferenciar si el objeto interactivo es la salida o un puzzle
    {
        if (!isEscape)
        {
            ActivarObjeto();
        }
        else
        {
            Victory();
        }
    }
}
