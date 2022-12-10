using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Sensores sensores;
    public PuzzleDeslizante puzzle;
    public GameObject fichaOculta;

    void Awake()
    {
        sensores = GetComponentInChildren(typeof(Sensores)) as Sensores;
        
    }


    public void OnMouseDown()
    {
        if (!sensores.ocupadoRight|| !sensores.ocupadoLeft|| !sensores.ocupadoDown|| !sensores.ocupadoUp)
        {
            Vector3 aux = fichaOculta.transform.position;
            fichaOculta.transform.position = gameObject.transform.position;
            gameObject.transform.position = aux;
            puzzle.EsGanador();

        }

    }


}
