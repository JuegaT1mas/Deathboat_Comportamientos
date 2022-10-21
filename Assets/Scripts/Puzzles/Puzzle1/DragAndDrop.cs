using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Sensores sensores;
    Puzzle puzzle;
    Vector3 mousePositionOffset;
    void Awake()
    {
        sensores = GetComponentInChildren(typeof(Sensores)) as Sensores;
        puzzle = GameObject.Find("Puzzle1").GetComponent(typeof(Puzzle)) as Puzzle;
    }


    private void OnMouseDown()
    {
        if (!sensores.ocupadoRight)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(1, 0, 0);
            puzzle.EsGanador();

        }
        else if (!sensores.ocupadoLeft)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(-1, 0, 0);
            puzzle.EsGanador();
        }
        else if (!sensores.ocupadoDown)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, -1, 0);
            puzzle.EsGanador();
        }
        else if (!sensores.ocupadoUp)
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
            puzzle.EsGanador();
        }


    }

}
