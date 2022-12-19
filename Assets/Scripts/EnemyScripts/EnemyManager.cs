using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("FOV")]
    public FieldOfView fov; //El script del cono de visión
    public FieldOfView longRangeFov; //El script del cono de visión

    [Header("Variables")]
    public float chaseSpeed; //La velocidad a la que te sigue
    public float minimumDistance; //La distancia mínima a la que gira una vez cerca del punto
    private GameObject playerRef; //La referencia al jugador
    private StarterAssetsInputs start; //La referencia a los starter assetsInputs
    private FirstPersonController first; //La referencia al firstpersonController
    //public float rotateSpeed;
    public GameObject[] minions; //Los minions a invocar


    [Header("Navigation")]
    public Transform[] points; //Los puntos del mapa donde va a patrullar el enemigo
    private NavMeshAgent agent; //El NavMesh del enemigo
    private int destPoint = 0; //Un integer para navegar el array de puntos


    [Header("Attack")]
    public Animator anim; //El animator del enemigo
    public float attackTimer; //El tiempo entre ataques
    public bool cooling; //Si esta descansando después de un ataque
    public float initialTimer; //El tiempo inicial
    public float attackDistanceMelee; //La distancia a partir de la cual te ataca a melee
    public float attackDistanceRange; //La distancia a partir de la cual te ataca a rango

    [Header("Rest")]
    public float restTime; //El tiempo que espera después de haber propinado un golpe al jugador
    public GameObject cubo; //El cubo que hace el daño

    [Header("percepciones")]
    public bool playerDetected = false; //Si el jugador esta detectado
    public bool seenFromAfar = false; //Si el jugador ha sido visto a lo lejos
    public bool playerHeard = false; //Si el jugador ha sido oído
    private bool checkForNear = false;
    private Vector3 lastPlayerPosition; //La última posición del jugador
    public bool puzzleCompletado = false;
    public float timeSincePlayerDetection = 0f; //Tiempo desde que no se vió al jugador
    public int hearingRadius = 4; //El rango en el que te oye
    public bool avisado = false; //Booleano para ser avisado por los minions


    private float MinionInvocationTime = 30f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackTimer = initialTimer; //Igualamos el tiempo
        GotoNextPoint(); //Ir al siguiente punto
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //Pillar el componente
        agent.autoBraking = false; //Para que cuando vaya a llegar al destino no frene bruscamente
        playerRef = fov.playerRef;
        start = playerRef.GetComponent<StarterAssets.StarterAssetsInputs>(); //Los startes assets
        first = playerRef.GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        EnemyBT();
    }

    private void EnemyBT()
    {
        //Actualizar Percepciones
        UpdateData();

        //Check Jugador Detectado
        if (playerDetected)
        {
            checkForNear = true; //Para comprobar el punto una vez perdamos de vista al jugador
            UpdatePlayerPosition();
            LookAt(false); //Rotamos al bicho para que mire al jugador

            timeSincePlayerDetection = 0f; //Al detectar al jugador reseteamos el tiempo

            float distanceToPlayer = Vector3.Distance(transform.position, lastPlayerPosition);
            
            //Gritar (no hay animación de momento)

            if (distanceToPlayer <= attackDistanceRange)//Check Jugador en Rango Distancia
            {
                if(distanceToPlayer <= attackDistanceMelee)//Check en Rango de Ataque Melee
                {
                    MeleeAttack();
                }
                else
                {
                    //RangeAttack();
                }
            }
            else
            {
                ChasePlayer(); //Que vaya hacia la posición del jugador
            }
        }else if (puzzleCompletado || avisado)//Check Avisado o Puzzle Completo
        {
            if (puzzleCompletado)
            {
                puzzleCompletado = false; //Lo volvemos a desactivar
            }
            if (avisado)
            {
                avisado = false;
            }
            UpdatePlayerPosition();
            ChasePlayer();

            timeSincePlayerDetection = 0f; //Al saber donde esta el jugador cuenta como detección
        }else if (seenFromAfar || playerHeard)//Check Escuchado o Visto
        {
            UpdatePlayerPosition();
            LookAt(true);
            GoToPoint(lastPlayerPosition); //Ir adonde se vió al jugador
        }
        else if (timeSincePlayerDetection >= MinionInvocationTime)//Check Invocar Diablos
        {
            timeSincePlayerDetection = 0f;
            InvokeMinions();
        }
        else //Patrullar
        {
            Patrullar();
        }
    }

    private void UpdateData()//Actualizar los datos que vamos a usar en el BT
    {
        playerDetected = PlayerDetected();
        seenFromAfar = PlayerSeen();
        playerHeard = PlayerHeard();
        TimeSincePlayerDetection();
    }

    private void UpdatePlayerPosition()
    {
        lastPlayerPosition = playerRef.transform.position; //Actualizamos la posición  
    }

    private bool PlayerDetected()//Actualizar si se detecta al jugador
    {
        if (fov.canSeePlayer)
        {
            return true;
        }
        return false;
    }

    private bool PlayerSeen()//Actualizar si se detecta al jugador
    {
        if (longRangeFov.canSeePlayer)
        {
            return true;
        }
        return false;
    }

    private bool PlayerHeard()
    {
        bool isMoving = start.move.sqrMagnitude != 0;
        bool inStealth = start.crouch;
        bool grounded = first.Grounded;

        Collider[] colliders = Physics.OverlapSphere(transform.position, hearingRadius, LayerMask.GetMask("Character"));//8 es la layer Character

        if(colliders.Length > 0 && ((isMoving && !inStealth) || !grounded))//Si estas al lado del bicho y estás moviendote (sin agacharse) o saltando
        {
            return true;
        }
        return false;
    }

    private void TimeSincePlayerDetection()
    {
        timeSincePlayerDetection += Time.deltaTime;
    }

    private void Patrullar()
    {
        if (!agent.pathPending && !agent.hasPath && agent.remainingDistance < minimumDistance)//Si no tiene un camino asignado
        {
            if (checkForNear)//Si acaba de perder al jugador de vista y necesita ver los puntos cercanos
            {
                NearestPoint(); //Calcula el punto de patrulla más cercano
                checkForNear = false;
            }
            //Si no tiene un camino pendiente y esta cerca del punto
            GotoNextPoint(); //Ir al siguiente punto
        }
    }

    private void LookAt(bool dis)//Función para mirar hacia el jugador
    {
        Vector3 direction;
        if (!dis)
        {
            direction = fov.targetDirection;
        }
        else
        {
            direction = longRangeFov.targetDirection;
        }
        
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

    private void GoToPoint(Vector3 point)
    {
        agent.destination = point;
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

    private void MeleeAttack()
    {
        if (cooling == false) //Si el monstruo esta cerca y no tiene cooldown
        {
            Attack(); //Ataca
        }

        if (cooling)//Si esta descansando entre ataques
        {
            //anim.SetBool("Attack", false); //Para la animación de ataque
            Cooldown(); //Pone en cooldown el ataque en sí
        }
    }

    private void ChasePlayer()
    {
        agent.destination = lastPlayerPosition; //Le decimos  donde esta el jugador y que vaya a esa posición
    }

    private void Attack() //Activa la animación de atacar
    {
        attackTimer = initialTimer;
        //anim.SetBool("mixamo.com", false); //Para la animación de ataque
        //anim.SetBool("Attack", true);//Comienza el ataque
        anim.Play("Attack_Walking");//Comienza el ataque
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

    private IEnumerator RestRoutine()
    {
        cubo.SetActive(false);
        this.enabled = false;
        WaitForSeconds wait = new WaitForSeconds(restTime);
        yield return wait;
        this.enabled = true;
        cubo.SetActive(true);
    }

    private IEnumerator WaitForMinions()
    {
        this.enabled = false;
        yield return new WaitUntil(CanContinueAvisado);
        this.enabled = true;
    }

    private bool CanContinueAvisado() => avisado;

    public void Rest()
    {
        StartCoroutine(RestRoutine());
    }

    public void InvokeMinions()
    {
        foreach (GameObject g in minions)
        {
            g.transform.position = transform.position;
            g.SetActive(true);
        }
        agent.ResetPath();
        StartCoroutine(WaitForMinions());
    }
}