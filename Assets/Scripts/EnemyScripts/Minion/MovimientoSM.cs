using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoSM : StateMachine
{
    //Poner los estados
    [HideInInspector]
    public Inicial inicialState;
    [HideInInspector]
    public Buscar buscarState;
    [HideInInspector]
    public Gritar gritarState;
    [HideInInspector]
    public Avisar avisarState;
    [HideInInspector]
    public Destruirse destruirState;
    [HideInInspector]
    public Vector3 lastPlayerPosition;

    [Header("ID Minions")]
    public int id;

    [Header("Referencias")]
    public EnemyManager diablo; //Referencia al monstruo
    public GameObject playerRef; //La referencia al jugador
    public NavMeshAgent agent; //El NavMesh del minion

    [Header("Buscar State Minion 1")]
    public FieldOfView fov; //El script del cono de visi�n
    public Transform[] points; //Los puntos del mapa donde va a patrullar el minion

    [Header("Buscar State Minion 3")]
    public Transform[] pointsPuzzles;
    public GameObject prefabMuebleFantasma;

    public float minimumDistance;

    [Header("Buscar State Minion 4")]
    public DisableShadows dis; //Para que el minion 4 apague las luces

    [Header("Gritar State")]
    public AudioSource gritoMinion;



    private void Awake()
    {
        inicialState = new Inicial(this);
        buscarState = new Buscar(this);
        gritarState = new Gritar(this);
        avisarState = new Avisar(this);
        destruirState = new Destruirse(this);
    }

    protected override BaseState GetInitialState()
    {
        return inicialState;
    }

    public void SerAvisado()
    {
        diablo.avisado = true;
    }

    public void InstanciarPuzzle(GameObject prefabMuebleFantasma, Vector3 position, Quaternion qua)
    {
        Instantiate(prefabMuebleFantasma, position, qua);
    }

}
