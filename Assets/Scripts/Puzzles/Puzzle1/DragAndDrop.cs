using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Sensores sensores;
    public PuzzleDeslizante puzzle;
    public GameObject fichaOculta;
    public int id;
    public int idPos;
    void Awake()
    {
        sensores = GetComponentInChildren(typeof(Sensores)) as Sensores;
        
    }


    public void OnMouseDown()
    {
        if (!sensores.ocupadoRight|| !sensores.ocupadoLeft|| !sensores.ocupadoDown|| !sensores.ocupadoUp)
        {
            int auxPosId = fichaOculta.GetComponent<DragAndDrop>().idPos;
            fichaOculta.GetComponent<DragAndDrop>().idPos = gameObject.GetComponent<DragAndDrop>().idPos;
            gameObject.GetComponent<DragAndDrop>().idPos = auxPosId;

            Vector3 auxPos = fichaOculta.transform.position;
            fichaOculta.transform.position = gameObject.transform.position;
            gameObject.transform.position = auxPos;
            puzzle.EsGanador();

        }

    }


}
