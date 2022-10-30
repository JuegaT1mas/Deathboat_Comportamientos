using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Sensores sensores;
    Puzzle1 puzzle;
    
    void Awake()
    {
        sensores = GetComponentInChildren(typeof(Sensores)) as Sensores;//Almacenamos el componente de los sensores
        puzzle = GameObject.Find("Puzzle1").GetComponent(typeof(Puzzle1)) as Puzzle1;//Almacenamos el componente Puzzle1
    }


    private void OnMouseDown()
    {
        //dependiendo de que sensor este libre la ficha se mueve a ese hueco
        Vector3 posAux;
        posAux = gameObject.transform.position;
        if (!sensores.ocupadoRight)
        {
           
            gameObject.transform.position = gameObject.transform.position + new Vector3(1, 0, 0);
           
           

        }
        else if (!sensores.ocupadoLeft)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(-1, 0, 0);
       
        }
        else if (!sensores.ocupadoDown)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, -1, 0);
            
        }
        else if (!sensores.ocupadoUp)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
            
        }
        puzzle.fichaEscondida.transform.position = posAux;
        puzzle.EsGanador();
    }

}
