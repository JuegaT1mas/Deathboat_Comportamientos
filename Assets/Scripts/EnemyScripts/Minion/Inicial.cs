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
        _sm.gameObject.SetActive(true);
       
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        
        //if(el bicho lleva mas de 2 minutos sin ver al jugador)
        //stateMachine.ChangeState(_sm.Buscar);
        if (_sm.prueba)
        {
            stateMachine.ChangeState(_sm.buscarState);
        }
    }
}
