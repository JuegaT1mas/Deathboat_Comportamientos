using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruirse : BaseState
{
    private MovimientoSM _sm;
    public Destruirse(MovimientoSM stateMachine) : base("Destruirse", stateMachine)
    {
        _sm = (MovimientoSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        stateMachine.ChangeState(_sm.inicialState);
        //Hacer algo hasta que, pase lo que tiene que pasar para cambiar de estado.
        //if(pasa algo)
        //stateMachine.ChangeState(((MovimientoSM)stateMachine).nombredelsiguienteState)
    }

    public override void Exit()
    {
        base.Exit();


        _sm.diablo.avisado = false;
        _sm.gameObject.SetActive(false);
    }
}
