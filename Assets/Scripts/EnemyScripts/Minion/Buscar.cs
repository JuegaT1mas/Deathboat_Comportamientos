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
        switch (_sm.id)
        {
            case 3:
                GotoNextPuzzle();
                break;
            default:
          
                break;
        }
    }

    public override void UpdateLogic()
    {        
        base.UpdateLogic();

        CheckAvisado();//Mirar si otro ha detectado el jugador antes

        switch (_sm.id)
        {
            case 4:
                break;
            default:
                playerDetected = _sm.fov.canSeePlayer;
                if (playerDetected) //Si se puede ver al jugador
                {
                    checkForNear = true;
                    UpdatePlayerPosition(); //Actualizamos la posición  
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
                PhysicsMinion2();
                break;
            case 3:
                PhysicsMinion3();
                break;
            case 4:
                PhysicsMinion4();
                break;
            case 5:
                PhysicsMinion5();
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

    private void UpdatePlayerPosition()
    {
        _sm.lastPlayerPosition = _sm.playerRef.transform.position; //Actualizamos la posición  
    }

    public void CheckAvisado()
    {
        if (_sm.diablo.avisado)
        {
            stateMachine.ChangeState(_sm.destruirState);
        }
    }

    private void ChasePlayer()
    {
        _sm.agent.SetDestination(_sm.lastPlayerPosition); //Le decimos  donde esta el jugador y que vaya a esa posición
    }

    public void PhysicsMinion1()//Se comporta igual que el enemigo grande
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

    public void PhysicsMinion3()//Se comporta igual que el enemigo grande
    {
        if (!_sm.agent.pathPending && !_sm.agent.hasPath && _sm.agent.remainingDistance < _sm.minimumDistance)
        {
            int indice = 6;
            if (destPoint != 0)
            {
                indice = destPoint - 1;
            }
            _sm.InstanciarPuzzle(_sm.prefabMuebleFantasma, _sm.pointsPuzzles[indice].transform.position, Quaternion.identity);
            //Si no tiene un camino pendiente y esta cerca del punto
            GotoNextPuzzle(); //Ir al siguiente punto
        }
    }

    public void GotoNextPuzzle()
    {
        // Si no hay puntos a los que ir no haz nada
        if (_sm.pointsPuzzles.Length == 0)
            return;

        // Hacer que el enemigo vaya hacia el siguiente punto
        _sm.agent.destination = _sm.pointsPuzzles[destPoint].position;

        // Pasar al siguiente punto, si es necesario volver a empezar desde el primer punto
        destPoint = (destPoint + 1) % _sm.pointsPuzzles.Length;
    }

    public void PhysicsMinion4()//Se acerca al jugador y le apaga las luces a su alrededor
    {
        UpdatePlayerPosition();
        ChasePlayer();
    }

    public void PhysicsMinion5()
    {
        if (!_sm.agent.pathPending && !_sm.agent.hasPath && _sm.agent.remainingDistance < _sm.minimumDistance)
        {
            //Si no tiene un camino pendiente y esta cerca del punto
            GotoRandomPoint(); //Ir al siguiente punto
        }
    }

    private void GotoRandomPoint()
    {
        bool pathValido = false;
        Vector3 destino = new Vector3(0,0,0);
        while (!pathValido)
        {
            float x = Random.Range(-54, 38);
            float y = Random.Range(-54, 38);
            float z = Random.Range(-3, 20);
            destino = new Vector3(x, y, z);

            pathValido = NavMesh.CalculatePath(_sm.transform.position, destino, NavMesh.AllAreas,new NavMeshPath());
        }
        _sm.agent.destination = destino;
    }

    public void PhysicsMinion2()//Sabe donde esta el jugador pero se mueve muy lento
    {
        UpdatePlayerPosition();
        ChasePlayer();
    }

}
