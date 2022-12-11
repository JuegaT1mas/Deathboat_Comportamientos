using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDeslizante : MonoBehaviour
{
    public List<GameObject> fichas = new List<GameObject>();
    List<Vector3> posicionesIniciales = new List<Vector3>();
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
        Vector3 pos1 = fichas[0].transform.position;
        Vector3 pos2 = fichas[1].transform.position;
        Vector3 pos3 = fichas[2].transform.position;
        Vector3 pos4 = fichas[3].transform.position;
        Vector3 pos5 = fichas[4].transform.position;
        Vector3 pos6 = fichas[5].transform.position;
        Vector3 pos7 = fichas[6].transform.position;
        Vector3 pos8 = fichas[7].transform.position;
        Vector3 pos9 = fichas[8].transform.position;


        Vector3[] posicionesMezcladas = { pos5, pos4, pos2, pos3, pos7, pos6, pos1, pos8, pos9 };
        Vector3[] posicionesMezcladas2 = { pos7, pos4, pos6, pos5, pos3, pos8, pos2, pos9, pos1 };
        Vector3[] posicionesMezcladas3 = { pos9, pos8, pos7, pos6, pos5, pos4, pos3, pos2, pos1 };
        Vector3[] posicionesMezcladas4 = { pos7, pos6, pos1, pos8, pos9, pos2, pos4, pos3, pos5 };
        Vector3[] posicionesMezcladas5 = { pos2, pos6, pos4, pos9, pos5, pos7, pos8, pos1, pos3 };
        Vector3[] posicionesMezcladas6 = { pos8, pos3, pos1, pos4, pos7, pos6, pos5, pos2, pos9 };
        Vector3[] posicionesMezcladas7 = { pos5, pos6, pos2, pos4, pos7, pos8, pos9, pos3, pos1 };

        List<Vector3[]> listas = new List<Vector3[]>();

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
            fichas[i].transform.position = listas[rand][i];
        }
    }

    public void EsGanador()
    {
        for (int i = 0; i < fichas.Count; i++)
        {
            if (posicionesIniciales[i] != fichas[i].transform.position)
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
