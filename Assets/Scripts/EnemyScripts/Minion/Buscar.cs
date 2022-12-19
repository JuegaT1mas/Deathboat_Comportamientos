using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buscar : BaseState
{
    private MovimientoSM _sm;
    private int destPoint = 0; //Un integer para navegar el array de punto
    private bool playerDetected = false; //Si el jugador esta detectado
    private bool checkForNear = false;
    
    public Buscar(MovimientoSM stateMachine) : base("Buscar", stateMachine){
        _sm = (MovimientoSM)stateMachine;
    }
  
    public override void Enter()
    {
        base.Enter();
        //Como empieece
    }

    public override void UpdateLogic()
    {        
        base.UpdateLogic();

        CheckAvisado();//Mirar si otro ha detectado el jugador antes

        playerDetected = _sm.fov.canSeePlayer;

        switch (_sm.id)
        {
            case 4:
                break;
            default:
                if (playerDetected) //Si se puede ver al jugador
                {
                    checkForNear = true;
                    _sm.lastPlayerPosition = _sm.fov.playerRef.transform.position; //Actualizamos la posición  
                    stateMachine.ChangeState(_sm.gritarState);
                }
                break;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        switch (_sm.id)
        {
            case 1:
                PhysicsMinion1();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }

    }

    public override void Exit()
    {
        _sm.gameObject.GetComponent<NavMeshAgent>().speed = 0f;
    }

    private void GotoNextPoint()
    {
        // Si no hay puntos a los que ir no haz nada
        if (_sm.points.Length == 0)
            return;

        // Hacer que el enemigo vaya hacia el siguiente punto
        _sm.agent.destination = _sm.points[destPoint].position;

        // Pasar al siguiente punto, si es necesario volver a empezar desde el primer punto
        destPoint = (destPoint + 1) % _sm.points.Length;
    }

    private void NearestPoint()
    {
        float distance = 99999;
        int index = 0;
        for (int i = 0; i < _sm.points.Length; i++) //Recorre todo el array y calcula el punto más cercano al actual
        {
            float d = Vector3.Distance(_sm.gameObject.transform.position, _sm.points[i].position);
            if (d < distance)
            {
                distance = d;
                index = i;
            }
        }
        destPoint = index;
    }

    public void CheckAvisado()
    {
        if (_sm.diablo.avisado)
        {
            stateMachine.ChangeState(_sm.destruirState);
        }
    }

    public void PhysicsMinion1()
    {
        if (!_sm.agent.pathPending && !_sm.agent.hasPath && _sm.agent.remainingDistance < _sm.minimumDistance)
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

}
