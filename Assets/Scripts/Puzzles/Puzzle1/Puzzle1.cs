using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Puzzle1 : PuzzlePadre
{
    public GameObject puzzleDeslizantePrefab;
    Vector3 posicion = new Vector3(0, 0, 0);

    public override void IniciarPuzzle()
    {
        GameObject puzzleDeslizante = Instantiate(puzzleDeslizantePrefab, posicion, Quaternion.identity);
        puzzleDeslizante.transform.parent = gameObject.transform;

    }

    public void PuzzleAcabado()
    {
        resuelto = true;
       
        Completed();
        Invoke("ShowInstructions", 1.5f);
    }
}

