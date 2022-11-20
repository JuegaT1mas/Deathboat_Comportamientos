using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : PuzzlePadre
{
    public GameObject tecladoPrefab;
    public GameObject notaPrefab;
    Vector3 posicion = new Vector3(0, -7, -22);
    // Start is called before the first frame update
    public override void IniciarPuzzle()
    {
        GameObject teclado = Instantiate(tecladoPrefab, posicion, Quaternion.identity);
        teclado.transform.parent = gameObject.transform;
        GameObject nota = Instantiate(notaPrefab, new Vector3(-8.07999992f, -19.2999992f, -27.2113323f), Quaternion.identity);
        nota.transform.parent = gameObject.transform;
    }

    public void PuzzleAcabado()
    {
        resuelto = true;
        Completed();
    }
}
