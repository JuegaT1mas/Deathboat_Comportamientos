using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicial : BaseState
{
    private MovimientoSM _sm;
    
    public Inicial(MovimientoSM stateMachine) : base("Inicial", stateMachine)
    {
        _sm = (MovimientoSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //_sm.gameObject.SetActive(true);
       
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        stateMachine.ChangeState(_sm.buscarState);
    }
}
