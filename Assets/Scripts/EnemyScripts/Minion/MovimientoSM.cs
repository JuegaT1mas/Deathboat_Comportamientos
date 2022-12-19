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

    [Header("Buscar State Minion 1")]
    public FieldOfView fov; //El script del cono de visión
    public Transform[] points; //Los puntos del mapa donde va a patrullar el minion
    public NavMeshAgent agent; //El NavMesh del minion
    public float minimumDistance;
    public bool prueba = false;

    [Header("Gritar State")]
    public AudioSource gritoMinion;



    private void Awake()
    {
        inicialState = new Inicial(this);
        buscarState = new Buscar(this);
        gritarState = new Gritar(this);
        avisarState = new Avisar(this);
        destruirState = new Destruirse(this);
        Invoke("Prueba", 5.0f);
    }

    private void Prueba()
    {
        prueba = true;
    }
    protected override BaseState GetInitialState()
    {
        return inicialState;
    }

    public void SerAvisado()
    {
        diablo.avisado = true;
    }

}
