using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Puzzle1 : PuzzlePadre
{
    public List<Sprite> fichaImg = new List<Sprite>();//Lista de sprites del puzzle
    //Se mete desde el inspector el prefab de la ficha y el borde
    public GameObject fichaPrefab;
    public GameObject bordePrefab;
    public Sprite fichaEscondidaImg;//la ficha que falta en el puzle

    int[,] puzzleMezclado;//La variable para comprobar que el puzle sea resolvible
    [HideInInspector]
    public GameObject fichaEscondida;//Separamos el gameObject de la ficha escondida para poder activarla y desactivarla
    int numCostado = 3;//sera 3 porque es un puzle 3x3
    //Los gameobjects que seran los padres de todos los gameobject ficha y borde
    GameObject padreFichas;
    GameObject padreBordes;
    public List<Vector3> posicionesIniciales = new List<Vector3>();//para saber si el puzzle está bien resuelto
    GameObject[] fichas;//array que almacena las fichas



    public override void IniciarPuzzle()//Este metodo para crear el puzzle
    {
        //recuperamos el padre de las fichas y de los bordes
        padreFichas = GameObject.Find("Fichas");
        padreBordes = GameObject.Find("Bordes");
        transform.position = new Vector3(0, -7, -22); //Posición del puzzle puesta a mano
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
        //vamos reyenando desde la esquina superior izquierda el puzle 3x3 incluyendo bordes
        for (int alto = numCostado + 1; alto >= 0; alto--)
        {
            for (int ancho = 0; ancho <= numCostado + 1; ancho++)
            {
                Vector3 posicion = new Vector3(ancho+transform.position.x, alto+transform.position.y, transform.position.z);//posición de cada ficha

                //si es un borde
                if (alto == 0 || alto == numCostado + 1 || ancho == 0 || ancho == numCostado + 1) 
                {
                    GameObject borde = Instantiate(bordePrefab, posicion, Quaternion.identity);//Instanciamos el borde
                    borde.transform.parent = padreBordes.transform;//Lo ponemos como hijo de PadreBordes

                }
                else//si es parte del puzzle
                {
                    GameObject ficha = Instantiate(fichaPrefab, posicion, Quaternion.identity);//Instanciamos la ficha
                    ficha.GetComponent<SpriteRenderer>().sprite = fichaImg[contador];//asignamos el sprite a cada ficha
                    ficha.transform.parent = padreFichas.transform;//lo ponemos como hijo de PadreFichas
                    ficha.name = fichaImg[contador].name;//le ponemos el nombre del sprite a cada ficha
                    if (ficha.name == fichaEscondidaImg.name)//comprobamos si es la ficha escondida
                    {
                        fichaEscondida = ficha;
                    }
                    contador++;//cuenta de las fichas que hemos creado
                }
            }
        }


        fichas = GameObject.FindGameObjectsWithTag("Ficha");//metemos todos los gameobject de las fichas dentro de un array
        for (int i = 0; i < fichas.Length; i++)
        {
            posicionesIniciales.Add(fichas[i].transform.position);//guardamos la posicion correcta del puzzle
        }
        fichaEscondida.gameObject.SetActive(false);//Desactivamos la ficha escondida

        //Mezclamos las fichas para que salgan desordenadas
        do
        {
            MezclarFichas();
        } while (!isSolvable(puzzleMezclado));//mezcla el puzle hasta que salga un puzle resolvible

    }


    void MezclarFichas()//Este metodo mezcla las fichas del puzle 
    {
        puzzleMezclado = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };//inicializamos las posiciones
        int random;
        for (int i = 0; i < fichas.Length; i++)//Para cada casilla del puzle, la mezclamos
        {
            random = Random.Range(0, fichas.Length);
            Vector3 pos = fichas[i].transform.position;//guardamos la posicion de la ficha
            fichas[i].transform.position = fichas[random].transform.position;//cambiamos la posicion de la ficha con otra ficha aleatoria
            fichas[random].transform.position = pos;//cambiamos la ficha aleatoria a la posicion de la otra

            //rellenamos el puzzleMezclado con los indices de las piezas
            int aux = puzzleMezclado[i % 3, i / 3];
            puzzleMezclado[i % 3, i / 3] = puzzleMezclado[random % 3, random / 3];
            puzzleMezclado[random % 3, random / 3] = aux;
        }
    }


    //https://www.geeksforgeeks.org/check-instance-8-puzzle-solvable/
    //Inicio metodos para comprobar si es resolvible
    static int getInvCount(int[] arr)
    {
        int inv_count = 0;
        for (int i = 0; i < 9 - 1; i++)
            for (int j = i + 1; j < 9; j++)

                // Value 0 is used for empty space
                if (arr[i] > 0 && arr[j] > 0 && arr[i] > arr[j])
                    inv_count++;
        return inv_count;
    }
    static bool isSolvable(int[,] puzzle)
    {
        int[] linearForm;
        linearForm = new int[9];
        int k = 0;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                linearForm[k++] = puzzle[i, j];

        // Count inversions in given 8 puzzle
        int invCount = getInvCount(linearForm);
        // return true if inversion count is even.
        return (invCount % 2 == 0);
    }
    //Final metodos para comprobar si es resolvible

    public void EsGanador()//Se llama cada vez que una pieza se mueve
    {
        //comprueba que las fichas esten colocadas en las posiciones correcta
        for (int i = 0; i < fichas.Length; i++)
        {
            if (posicionesIniciales[i] != fichas[i].transform.position)
            {
                return;
            }
        }

        fichaEscondida.gameObject.SetActive(true);
        print("Puzzle resuelto");
        resuelto = true;
       
        Completed();
        
    }
}
