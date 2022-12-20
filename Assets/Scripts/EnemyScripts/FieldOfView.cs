using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius; //El radio del campo de visión
    [Range(0, 360)]
    public float angle; //El rango del ángulo de visión

    public GameObject playerRef; //Referencia al jugador

    public LayerMask targetMask; //La capa que observa 
    public LayerMask obstructionMask; //Las capas que van a actuar de obstáculos

    public bool canSeePlayer; //Booleano si puede ver al jugador

    public Vector3 targetDirection;  //La dirección a mirar

    public float checkDelay = 0.2f; //Cada cuanto va a ejecutar la corrutina

    public void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player"); //Encontrar la referencia al jugador al empezar
    }

    private void OnEnable()
    {
        StartCoroutine(FOVRoutine()); //Comenzar la corrutina
    }

    private IEnumerator FOVRoutine() //Crear una corrutina para buscar al jugador
    {
        WaitForSeconds wait = new WaitForSeconds(checkDelay);//El delay de cada cuanto va a comprobar la corrutina que se busque al jugador
        while (true) //El true se puede cambiar por una variable para decidir si se quiere buscar o no
        {
            yield return wait; //Esperar el tiempo del delay
            FieldOfViewCheck(); //Metodo de comprobación de la presencia del jugador
        }
    }

    private void FieldOfViewCheck()
    {
        //Comprueba los colliders que se sobreponen a la esfera de radio definido antes en la máscara de capa específica
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        //Mirar como funciona porque a lo mejor hay que cambiar el esfera por una caja del tamaño del techo para que no mire en otros pisos.
    
        if(rangeChecks.Length != 0) //Si hemos encontrado una colisión, por la naturaleza del juego este solo va a ser el jugador
        {
            Transform target = rangeChecks[0].transform; //pillamos el transform del jugador
            Vector3 directionToTarget = (target.position - transform.position).normalized; //Miramos la dirección hacia el objetivo

            targetDirection = directionToTarget; //Actualizamos la dirección

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) //Si el ángulo de visión del enemigo ve al jugador
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position); //la distancia al jugador

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                //Si lanzamos un rayo mirando hacia el jugador y con la distancia al jugador y no se choca con nada entonces le vemos
                {
                    canSeePlayer = true;
                }
                else //Si no le vemos
                {
                    canSeePlayer = false;
                }
            }
            else //El jugador no esta dentro del campo de visión del enemigo
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer) //Si no recibimos ningún collider
        {
            canSeePlayer = false; //entonces no le estamos viendo
        }
    }
}
