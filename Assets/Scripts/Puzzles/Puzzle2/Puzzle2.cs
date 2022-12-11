using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle2 : PuzzlePadre
{
    
    public GameObject textoPrefab;
    public GameObject brujulaPrefab;
 
    GameObject flecha;
    GameObject botonRight;
    GameObject botonUp;
    GameObject botonLeft;
    GameObject botonDown;
    GameObject brujula;
    TextMeshProUGUI textoNota;

    Vector3 posicion = new Vector3(2, -4, -20);
    GameObject puzzle;
    Rigidbody2D rbFlecha;
    public bool estaGirando = false;

    bool puzzleAcabado = false;
    public bool botonPulsado = false;

    public int aciertos = 0;

    public int dirFlecha = 0;
    int random;

    // Start is called before the first frame update

    private void Awake()
    {
        puzzle = GameObject.Find("Puzzle2");
        brujula=Instantiate(brujulaPrefab, posicion, Quaternion.identity);
     

        brujula.transform.parent = puzzle.transform;

        textoNota = brujula.GetComponentInChildren<TextMeshProUGUI>();
        flecha = GameObject.Find("Flecha");
        rbFlecha = flecha.GetComponent<Rigidbody2D>();

        
        flecha.SetActive(true);
        puzzle.SetActive(false);
       

    }
    public override void IniciarPuzzle()
    {
        puzzle.SetActive(true);
        crearBotones();
       
    }


    void crearBotones()
    {
        botonRight = GameObject.Find("BotonE");
        botonUp = GameObject.Find("BotonN");
        botonLeft =GameObject.Find("BotonW");
        botonDown = GameObject.Find("BotonS");
       

        botonRight.GetComponent<Boton>().id = 1;
        botonUp.GetComponent<Boton>().id = 2;
        botonLeft.GetComponent<Boton>().id = 3;
        botonDown.GetComponent<Boton>().id = 4;

    
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
        if (aciertos == 0)
        {
            textoNota.text = "Too slow!";
        }
        else
        {
            textoNota.text = aciertos + " / 4";
        }
        if (!botonPulsado)
        {
            
            aciertos = 0;
           
        }
    }
    void girarFlecha()
    {
        rbFlecha.angularVelocity = Random.Range(500, 600);
    }

    public void esGanador()
    {
        if (aciertos == 4)
        {
            rbFlecha.angularVelocity = 0;
            puzzleAcabado = true;

            Invoke("Desactivar", 1.0f);
            Invoke("ShowInstructions", 1.0f);

            resuelto = true;
            Completed();
           
        }
    }

    public void Desactivar()
    {
        brujula.SetActive(false);
    }
}