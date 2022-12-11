using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDeslizante : MonoBehaviour
{
    public List<GameObject> fichas = new List<GameObject>();
    public List<Vector3> posicionesIniciales = new List<Vector3>();
    public GameObject fichaEscondida;
    GameObject puzzle1;

    private void Start()
    {
        puzzle1 = GameObject.Find("Puzzle1");
        for (int i = 0; i < fichas.Count; i++)
        {
            posicionesIniciales.Add(fichas[i].transform.position);
        }
        MezclarFichas();
    }


    void MezclarFichas()
    {
        GameObject pos1 = fichas[0];
        GameObject pos2 = fichas[1];
        GameObject pos3 = fichas[2];
        GameObject pos4 = fichas[3];
        GameObject pos5 = fichas[4];
        GameObject pos6 = fichas[5];
        GameObject pos7 = fichas[6];
        GameObject pos8 = fichas[7];
        GameObject pos9 = fichas[8];

        

        GameObject[] posicionesMezcladas = { pos5, pos4, pos2, pos3, pos7, pos6, pos1, pos8, pos9 };
        GameObject[] posicionesMezcladas2 = { pos7, pos4, pos6, pos5, pos3, pos8, pos2, pos9, pos1 };
        GameObject[] posicionesMezcladas3 = { pos9, pos8, pos7, pos6, pos5, pos4, pos3, pos2, pos1 };
        GameObject[] posicionesMezcladas4 = { pos7, pos6, pos1, pos8, pos9, pos2, pos4, pos3, pos5 };
        GameObject[] posicionesMezcladas5 = { pos2, pos6, pos4, pos9, pos5, pos7, pos8, pos1, pos3 };
        GameObject[] posicionesMezcladas6 = { pos8, pos3, pos1, pos4, pos7, pos6, pos5, pos2, pos9 };
        GameObject[] posicionesMezcladas7 = { pos5, pos6, pos2, pos4, pos7, pos8, pos9, pos3, pos1 };

        List<GameObject[]> listas = new List<GameObject[]>();
        List<Vector3> posiciones = new List<Vector3>();
        List<int> ids= new List<int>();

        listas.Add(posicionesMezcladas);
        listas.Add(posicionesMezcladas2);
        listas.Add(posicionesMezcladas3);
        listas.Add(posicionesMezcladas4);
        listas.Add(posicionesMezcladas5);
        listas.Add(posicionesMezcladas6);
        listas.Add(posicionesMezcladas7);

        int rand = Random.Range(0, listas.Count);

        for (int i = 0; i < fichas.Count; i++)
        {
            posiciones.Add(listas[rand][i].transform.position);
            ids.Add(listas[rand][i].GetComponent<DragAndDrop>().idPos);            
        }

        for (int i = 0; i < posiciones.Count; i++)
        {
            fichas[i].transform.position = posiciones[i];
            fichas[i].GetComponent<DragAndDrop>().idPos = ids[i];
        }
       
    }

    public void EsGanador()
    {
        for (int i = 0; i < fichas.Count; i++)
        {
            if (fichas[i].GetComponent<DragAndDrop>().id!=fichas[i].GetComponent<DragAndDrop>().idPos)
            {
              return;
            }
        }
        fichaEscondida.gameObject.SetActive(true);
        print("Puzzle resuelto");

        Invoke("DesactivarTodo", 1.5f);
        puzzle1.GetComponent<Puzzle1>().PuzzleAcabado();
    }

    void DesactivarTodo() {
        gameObject.SetActive(false);
    }

}
