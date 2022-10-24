using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("FOV")]
    public FieldOfView fov; //El script del cono de visión

    [Header("Variables")]
    public float chaseSpeed; //La velocidad a la que te sigue
    public float minimumDistance; //La distancia mínima a la que gira una vez cerca del punto
    //public float rotateSpeed;



    [Header("Navigation")]
    public Transform[] points; //Los puntos del mapa donde va a patrullar el enemigo
    private NavMeshAgent agent; //El NavMesh del enemigo
    private int destPoint = 0; //Un integer para navegar el array de puntos
    private Vector3 lastPlayerPosition; //La última posición del jugador
    public bool playerDetected = false; //Si el jugador esta detectado
    private bool checkForNear = false;

    [Header("Attack")]
    public Animator anim; //El animator del enemigo
    public float attackTimer; //El tiempo entre ataques
    public bool cooling; //Si esta descansando después de un ataque
    public float initialTimer; //El tiempo inicial
    public float attackDistance; //La distancia a partir de la cual te ataca

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackTimer = initialTimer; //Igualamos el tiempo
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Pillar el componente
        agent.autoBraking = false; //Para que cuando vaya a llegar al destino no frene bruscamente
        GotoNextPoint(); //Ir al siguiente punto
    }

    private void Update()
    {
        playerDetected = fov.canSeePlayer;

        if (playerDetected) //Si se puede ver al jugador
        {
            checkForNear = true;
            lastPlayerPosition = fov.playerRef.transform.position; //Actualizamos la posición  
            LookAt();
            ChasePlayer();
        }
        else if (!agent.pathPending && agent.remainingDistance < minimumDistance)
        {
            if (checkForNear)
            {
                NearestPoint();
                checkForNear = false;
            }
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
        agent.destination = lastPlayerPosition; //Le decimos donde esta el jugador
        if (Vector3.Distance(transform.position, lastPlayerPosition) < attackDistance && cooling == false) //Si el monstruo esta cerca y no tiene cooldown
        {
            Attack(); //Ataca
        }
        
        if (cooling)//Si esta descansando entre ataques
        {
            anim.SetBool("Attack", false); //Para la animación de ataque
            Cooldown(); //Pone en cooldown el ataque en sí
        }
    }

    private void Attack() //Activa la animación de atacar
    {
        attackTimer = initialTimer;
        anim.SetBool("Attack", true);//Comienza el ataque
    }

    private void Cooldown()//Recarga el tiempo de ataque
    {
        attackTimer -= Time.deltaTime; //Reduce el tiempo de cooldown
        if(attackTimer <= 0 && cooling == true) //Si ha llegado a 0 el contador y cooling es true
        {
            cooling = false;//Lo pone a false, dejando así que pueda volver a atacar el bicho
        }
    }

    public void TriggerCooling()//Función que activa el animator tras acabar un ataque
    {
        cooling = true; //Pone en cooldown el ataque
    }
}