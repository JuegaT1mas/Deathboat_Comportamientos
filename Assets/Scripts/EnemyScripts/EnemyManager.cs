using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public FieldOfView fov; //El script del cono de visión

    public float chaseSpeed; //La velocidad a la que te sigue
    public float minimumDistance; //La distancia mínima a la que queremos que este el monstruo
    //public float rotateSpeed;

    private Vector3 lastPlayerPosition; //La última posición del jugador

    public bool playerDetected = false; //Si el jugador esta detectado

    public Transform[] points; //Los puntos del mapa donde va a patrullar el enemigo
    private NavMeshAgent agent; //El NavMesh del enemigo
    private int destPoint = 0; //Un integer para navegar el array de puntos


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

    private void Update()
    {
        playerDetected = fov.canSeePlayer;

        if (playerDetected) //Si se puede ver al jugador
        {
            lastPlayerPosition = fov.playerRef.transform.position; //Actualizamos la posición  
            LookAt();
            ChasePlayer();
        }
        else if (!agent.pathPending && agent.remainingDistance < minimumDistance)
        {
            //Si no tiene un camino pendiente y esta cerca del punto
            GotoNextPoint(); //Ir al siguiente punto
        }
    }

    private void LookAt()//Función para mirar hacia el jugador
    {
        Vector3 direction = fov.targetDirection; 
        direction.y = 0;  //Para poder mirar al jugador sin que el monstruo se incline bloqueamos la y de la rotación.
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void NearestPoint()
    {
        float distance = 99999;
        int index = 0;
        for (int i = 0; i < points.Length; i++) //Recorre todo el array y calcula el punto más cercano al actual
        {
            float d = Vector3.Distance(transform.position, points[i].position);
            if(d < distance)
            {
                distance = d;
                index = i;
            }
        }
        destPoint = index;
    }

    private void GotoNextPoint()
    {
        // Si no hay puntos a los que ir no haz nada
        if (points.Length == 0)
            return;

        // Hacer que el enemigo vaya hacia el siguiente punto
        agent.destination = points[destPoint].position;

        // Pasar al siguiente punto, si es necesario volver a empezar desde el primer punto
        destPoint = (destPoint + 1) % points.Length;
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, lastPlayerPosition) > minimumDistance) //Comprobamos que no se acerque demasiado
        {
            agent.destination = lastPlayerPosition;
        }
    }
}