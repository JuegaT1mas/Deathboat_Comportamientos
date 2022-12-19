using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class Gritar : BaseState
{
    private MovimientoSM _sm;
    private float tiempoAEsperar=3f;
    private float tiempo = 0;
    public Gritar(MovimientoSM stateMachine) : base("Gritar", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.gritoMinion.Play();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
             
  
        if (tiempo < tiempoAEsperar)
        {
            tiempo += Time.deltaTime;
        }
        else
        {
            CheckAvisado();
            stateMachine.ChangeState(_sm.avisarState);
        }
       

    }

    public void CheckAvisado()
    {
        if (_sm.diablo.avisado)
        {
            stateMachine.ChangeState(_sm.destruirState);
        }
    }
}
