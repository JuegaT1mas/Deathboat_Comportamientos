using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<Sprite> fichaImg = new List<Sprite>();//Lista de sprites del puzzle
    public GameObject fichaPrefab;
    public GameObject bordePrefab;
    public Sprite fichaEscondidaImg;//la ficha que falta en el puzle



    GameObject fichaEscondida;
    int numCostado = 3;//sera 3 porque es un puzle 3x3
    GameObject padreFichas;
    GameObject padreBordes;
    List<Vector3> posicionesIniciales = new List<Vector3>();//para saber si el puzzle está bien resuelto
    GameObject[] fichas;


    public void IniciarPuzzle()
    {
        //recuperamos el padre de las fichas y de los bordes
        padreFichas = GameObject.Find("Fichas");
        padreBordes = GameObject.Find("Bordes");
        CrearFichas();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void CrearFichas()
    {
        int contador = 0;//Para poner en cada ficha el sprite correspondiente

        //Doble bucle para colocar las fichas en la matriz
        for (int alto = numCostado + 1; alto >= 0; alto--)
        {

            for (int ancho = 0; ancho <= numCostado + 1; ancho++)
            {
                Vector3 posicion = new Vector3(ancho, alto, 0);//posición de cada ficha

                //Para ver si es un borde o no
                if (alto == 0 || alto == numCostado + 1 || ancho == 0 || ancho == numCostado + 1) //Es parte del borde
                {
                    GameObject borde = Instantiate(bordePrefab, posicion, Quaternion.identity);//Instanciamos el borde
                    borde.transform.parent = padreBordes.transform;//Lo ponemos cómo hijo de PadreBordes

                }
                else//Es parte del puzzle
                {
                    GameObject ficha = Instantiate(fichaPrefab, posicion, Quaternion.identity);//Instanciamos la ficha
                    ficha.GetComponent<SpriteRenderer>().sprite = fichaImg[contador];//asignamos el sprite a cada ficha
                    ficha.transform.parent = padreFichas.transform;
                    ficha.name = fichaImg[contador].name;
                    if (ficha.name == fichaEscondidaImg.name)
                    {
                        fichaEscondida = ficha;
                    }
                    contador++;
                }
            }
        }
        fichaEscondida.gameObject.SetActive(false);

        fichas = GameObject.FindGameObjectsWithTag("Ficha");//metemos todos los gameobject de las fichas dentro de un array
        for (int i = 0; i < fichas.Length; i++)
        {
            posicionesIniciales.Add(fichas[i].transform.position);//guardamos la posicion correcta del puzzle
        }
        //Mezclamos las fichas para que salgan desordenadas
        MezclarFichas();
    }


    void MezclarFichas()
    {
        int random;
        for (int i = 0; i < fichas.Length; i++)
        {
            random = Random.Range(0, fichas.Length);
            Vector3 pos = fichas[i].transform.position;
            fichas[i].transform.position = fichas[random].transform.position;
            fichas[random].transform.position = pos;
        }
    }

    public void EsGanador()
    {
        for (int i = 0; i < fichas.Length; i++)
        {
            if (posicionesIniciales[i] != fichas[i].transform.position)
            {
                return;
            }
        }
        fichaEscondida.gameObject.SetActive(true);
        print("Puzzle resuelto");

    }
}
