using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : PuzzlePadre
{
    public GameObject tecladoPrefab;

    Vector3 posicion = new Vector3(0, -7, -22);
    // Start is called before the first frame update
    public override void IniciarPuzzle()
    {
        GameObject teclado = Instantiate(tecladoPrefab, posicion, Quaternion.identity);
        teclado.transform.parent = gameObject.transform;

    }

    public void PuzzleAcabado()
    {
        resuelto = true;
        Completed();
    }
}
