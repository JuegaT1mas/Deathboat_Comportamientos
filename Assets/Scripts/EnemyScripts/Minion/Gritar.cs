using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gritar : BaseState
{
    private MovimientoSM _sm;
    public Gritar(MovimientoSM stateMachine) : base("Gritar", stateMachine)
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

        //Hacer algo hasta que, pase lo que tiene que pasar para cambiar de estado.
        //if(pasa algo)
        //stateMachine.ChangeState(((MovimientoSM)stateMachine).nombredelsiguienteState)
    }
}
