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
        //Esperar 3 segundos
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        CheckAvisado();
        //Gritar;
        stateMachine.ChangeState(_sm.avisarState);
    }

    public void CheckAvisado()
    {
        if (_sm.diablo.avisado)
        {
            stateMachine.ChangeState(_sm.destruirState);
        }
    }
}
