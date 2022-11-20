using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : PuzzlePadre
{
    public GameObject flechaPrefab;
    public GameObject textoPrefab;
    public GameObject brujulaPrefab;
    public GameObject notaPrefab;


    GameObject flecha;
    GameObject botonRight;
    GameObject botonUp;
    GameObject botonLeft;
    GameObject botonDown;
    GameObject brujula;

    Vector3 posicion = new Vector3(2, -4, -20);
    GameObject puzzle;
    Rigidbody2D rbFlecha;
    public bool estaGirando = false;

    bool puzzleAcabado = false;
    public bool botonPulsado = false;

    public int aciertos = 0;

    public int dirFlecha = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        puzzle = GameObject.Find("Puzzle2");
        brujula=Instantiate(brujulaPrefab, posicion, Quaternion.identity);
     

        brujula.transform.parent = puzzle.transform;
      

        flecha = GameObject.Find("FlechaPrefab");
        rbFlecha = flecha.GetComponent<Rigidbody2D>();
        flecha.transform.parent = puzzle.transform;

        flecha.SetActive(true);
        puzzle.SetActive(false);
        
    }
    public override void IniciarPuzzle()
    {
        puzzle.SetActive(true);
        GameObject nota = Instantiate(notaPrefab, new Vector3(-2, -4, -22), Quaternion.identity);
        nota.transform.parent = gameObject.transform;
   
        crearBotones();
    }


    void crearBotones()
    {
        botonRight = GameObject.Find("BotonPrefabW");
        botonUp = GameObject.Find("BotonPrefabN");
        botonLeft =GameObject.Find("BotonPrefabE");
        botonDown = GameObject.Find("BotonPrefabS");

        botonRight.GetComponent<Boton>().id = 1;
        botonUp.GetComponent<Boton>().id = 2;
        botonLeft.GetComponent<Boton>().id = 3;
        botonDown.GetComponent<Boton>().id = 4;

        botonRight.transform.parent = puzzle.transform;
        botonUp.transform.parent = puzzle.transform;
        botonLeft.transform.parent = puzzle.transform;
        botonDown.transform.parent = puzzle.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (!puzzleAcabado)
        {
            if (!estaGirando)
            {
                estaGirando = true;
                girarFlecha();

            }
            else
            {
                if (rbFlecha.angularVelocity <= 450)
                {
                    Quaternion pos = rbFlecha.transform.rotation;
                    Vector3 angles = pos.eulerAngles;

                    if (angles.z <= 45 && angles.z >= 0 || angles.z > 315)
                    {
                        rbFlecha.rotation = 0;
                        dirFlecha = 1;
                    }
                    else if (angles.z <= 135 && angles.z > 45)
                    {
                        rbFlecha.rotation = 90;
                        dirFlecha = 2;
                    }
                    else if (angles.z <= 225 && angles.z > 135)
                    {
                        rbFlecha.rotation = 180;
                        dirFlecha = 3;
                    }
                    else if (angles.z <= 315 && angles.z > 225)
                    {
                        rbFlecha.rotation = 270;
                        dirFlecha = 4;
                    }
                    rbFlecha.angularVelocity = 0;
                    botonPulsado = false;
                    Invoke("ReinicioPuzzle", 1f);

                }
            }
        }

    }

    void ReinicioPuzzle()
    {
        if (!botonPulsado)
        {
            aciertos = 0;

        }
    }
    void girarFlecha()
    {
        rbFlecha.angularVelocity = 500f;
    }

    public void esGanador()
    {
        if (aciertos == 4)
        {
            rbFlecha.angularVelocity = 0;
            puzzleAcabado = true;
            GameObject texto = Instantiate(textoPrefab, posicion, Quaternion.identity);

            texto.transform.parent = puzzle.transform;

            botonRight.SetActive(false);
            botonUp.SetActive(false);
            botonLeft.SetActive(false);
            botonDown.SetActive(false);
            brujula.SetActive(false);
            flecha.SetActive(false);

            resuelto = true;
            Completed();
           
        }
    }
}